namespace Negocio.DTOs
{
    public class VentaRequestDTO
    {
        public int IdCliente { get; set; }
        public string? MetodoDePago { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public List<DetalleVentaRequestDTO> DetalleVenta { get; set; } = new();
    }
}
