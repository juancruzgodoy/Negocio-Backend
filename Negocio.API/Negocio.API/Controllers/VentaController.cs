using Negocio;
using Negocio.Interfaces;
using Negocio.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Negocio.DTOs;


namespace Negocio.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentaController : ControllerBase
    {

        private readonly IVentaService _ventaService;
        public VentaController(IVentaService ventaService)
        {
            _ventaService = ventaService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VentaDTO>> Get(int id)
        {
            try
            {
                var venta = await _ventaService.GetVentaByIdAsync(id);
                if (venta == null)
                {
                    return NotFound($"No se encontró una venta con el ID {id}.");
                }
                var VentaDTO = new VentaDTO
                {
                    Id = venta.Id,
                    Fecha = venta.Fecha,
                    TotalVenta = venta.TotalVenta,
                    DetalleVenta = venta.DetalleVenta.Select(dv => new DetalleVentaDTO
                    {
                        Id = dv.Id,
                        IdProducto = dv.IdProducto,
                        Cantidad = dv.Cantidad,
                        PrecioUnitario = dv.PrecioUnitario,
                        Subtotal = dv.Subtotal
                    }).ToList()
                };
                return Ok(VentaDTO);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error interno al procesar la solicitud.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<VentaDTO>> CrearVentaAsync([FromBody] VentaRequestDTO ventaRequestDto)
        {
            if (ventaRequestDto == null)
            {
                return BadRequest("La venta no puede ser nula.");
            }
            try
            {
                var nuevaVenta = await _ventaService.CrearVentaAsync(ventaRequestDto);
                return CreatedAtAction(nameof(Get), new { id = nuevaVenta.Id }, nuevaVenta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error interno al procesar la venta: {ex.Message}");
            }
        }
    }
}
