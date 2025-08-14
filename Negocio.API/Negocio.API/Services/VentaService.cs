using Negocio.DTOs;
using Negocio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Negocio.API.Services
{
    public class VentaService: IVentaService
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IVentaRepository _ventaRepository;
        private readonly NegocioContext _context;

        public VentaService(IVentaRepository ventaRepository, IProductoRepository productoRepository, NegocioContext context)
        {
            _productoRepository = productoRepository;
            _ventaRepository = ventaRepository;
            _context = context;
        }

        public async Task<VentaDTO?> GetVentaByIdAsync(int id)
        {
            var venta = await _context.Venta.Include(v => v.DetalleVenta).FirstOrDefaultAsync(v => v.Id == id);

            if (venta == null)
            {
                return null;
            }
            
            var ventaDTO = new VentaDTO
            {
                Id = venta.Id,
                IdCliente = venta.IdCliente,
                Fecha = venta.Fecha,
                TotalVenta = venta.TotalVenta,
                MetodoDePago = venta.MetodoDePago,
                DetalleVenta = venta.DetalleVenta.Select(dv => new DetalleVentaDTO
                {
                    Id = dv.Id,
                    IdProducto = dv.IdProducto ?? 0,
                    Cantidad = dv.Cantidad ?? 0,
                    PrecioUnitario = dv.PrecioUnitario,
                    Subtotal = dv.Subtotal
                }).ToList()
            };

            return ventaDTO;
        }

        public async Task<VentaDTO> CrearVentaAsync(VentaRequestDTO ventaRequestDTO)
        {
            //creo un array de IDs de productos para validar stock
            var productoIds = ventaRequestDTO.DetalleVenta.Select(detalle => detalle.IdProducto).ToList();
            var productosDeLaVenta = await _context.Productos
                .Where(p => productoIds.Contains(p.Id)).ToListAsync();
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    if (ventaRequestDTO == null)
                    {
                        throw new ArgumentNullException(nameof(ventaRequestDTO), "La solicitud de venta no puede ser nula.");
                    }
                    //Validar que haya detalles de venta
                    if (ventaRequestDTO.DetalleVenta == null || !ventaRequestDTO.DetalleVenta.Any())
                    {
                        throw new Exception("La venta debe tener al menos un detalle.");
                    }
                    //Validar stock
                    foreach (var detalle in ventaRequestDTO.DetalleVenta)
                    {
                        var producto = productosDeLaVenta.FirstOrDefault(p => detalle.IdProducto == p.Id);
                        if (producto == null)
                        {
                            throw new Exception($"Producto con ID {detalle.IdProducto} no disponible.");
                        }
                        if (producto.StockActual < detalle.Cantidad)
                        {
                            throw new Exception($"Producto con ID {detalle.IdProducto} no tiene stock suficiente.");
                        }
                    }                    
                    //Calculo el total de la venta
                    decimal TotalVenta = 0;
                    foreach (var detalleDto in ventaRequestDTO.DetalleVenta)
                    {
                        var producto = productosDeLaVenta.First(p => p.Id == detalleDto.IdProducto);
                        TotalVenta += (producto.PrecioVenta ?? 0) * (decimal)detalleDto.Cantidad;
                    }
                    // Crear la venta
                    var venta = new Venta
                    {
                        IdCliente = ventaRequestDTO.IdCliente,
                        Fecha = ventaRequestDTO.Fecha,
                        TotalVenta = TotalVenta,
                        MetodoDePago = ventaRequestDTO.MetodoDePago
                    };
                    // Agregar detalles de la venta
                    if (ventaRequestDTO.DetalleVenta != null)
                    {
                        foreach (var detalle in ventaRequestDTO.DetalleVenta)
                        {
                            var producto = productosDeLaVenta.First(p => detalle.IdProducto == p.Id);
                            var detalleVenta = new DetalleVenta
                            {
                                IdProducto = detalle.IdProducto,
                                Cantidad = detalle.Cantidad,
                                PrecioUnitario = producto.PrecioVenta,
                                Subtotal = (producto.PrecioVenta) * (decimal)detalle.Cantidad
                            };
                            // Actualizar el stock del producto
                            producto.StockActual -= detalle.Cantidad;
                            _context.Productos.Update(producto);
                            venta.DetalleVenta.Add(detalleVenta);
                        }
                    }
                    _context.Venta.Add(venta);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    // Asignar el Id generado al DTO
                    var VentaDto = new VentaDTO
                    {
                        IdCliente = venta.IdCliente,
                        Fecha = venta.Fecha,
                        TotalVenta = venta.TotalVenta,
                        MetodoDePago = venta.MetodoDePago,
                        DetalleVenta = venta.DetalleVenta.Select(dv => new DetalleVentaDTO
                        {
                            Id = dv.Id,
                            IdProducto = dv.IdProducto ?? 0,
                            Cantidad = dv.Cantidad ?? 0,
                            PrecioUnitario = dv.PrecioUnitario,
                            Subtotal = dv.Subtotal
                        }).ToList()
                    };
                    return VentaDto;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
            
        }
    }
}