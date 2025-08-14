namespace Negocio.DTOs
{
    public class VentaDTO
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public DateTime Fecha { get; set; }
        public decimal? TotalVenta { get; set; }
        public string? MetodoDePago { get; set; }
        public List<DetalleVentaDTO> DetalleVenta { get; set; } = new List<DetalleVentaDTO>();
    }
}