using Negocio;
using Negocio.Interfaces;
using Negocio.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Negocio.DTOs;

[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly IProductoRepository _productoRepository;

    public ProductosController(IProductoRepository productoRepository)
    {
        _productoRepository = productoRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductoDTO>>> Get()
    {
        var productos = await _productoRepository.GetAllAsync();
        var productoDto = productos.Select(p => new ProductoDTO
        {
            Id = p.Id,
            Nombre = p.Nombre,
            UnidadDeMedida = p.UnidadDeMedida,
            PrecioVenta = p.PrecioVenta,
            StockActual = p.StockActual,
            IdCategoria = p.IdCategoria ?? 0,
            IdProveedor = p.IdProveedor ?? 0,
        }).ToList();

        return Ok(productoDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductoDTO>> Get(int id)
    {
        var producto = await _productoRepository.GetByIdAsync(id);
        if (producto == null)
        {
            return NotFound();
        }
        var productoDto = new ProductoDTO
        {
            Id = producto.Id,
            Nombre = producto.Nombre,
            UnidadDeMedida = producto.UnidadDeMedida,
            PrecioVenta = producto.PrecioVenta,
            StockActual = producto.StockActual,
            IdCategoria = producto.IdCategoria ?? 0,
            IdProveedor = producto.IdProveedor ?? 0,
        };
        return Ok(productoDto);
    }

    [HttpPost]
    public async Task<ActionResult<Producto>> Post([FromBody] Producto nuevoProducto)
    {
        if (nuevoProducto == null)
        {
            return BadRequest("Producto no puede ser nulo.");
        }
        await _productoRepository.AddAsync(nuevoProducto);
        await _productoRepository.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = nuevoProducto.Id }, nuevoProducto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Producto productoActualizado)
    {
        if (id != productoActualizado.Id)
        {
            return BadRequest();
        }

        _productoRepository.Update(productoActualizado);

        try
        {
            await _productoRepository.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var producto = await _productoRepository.GetByIdAsync(id);
        if (producto == null)
        {
            return NotFound();
        }

        _productoRepository.Delete(producto);
        await _productoRepository.SaveChangesAsync();

        return NoContent();
    }
}