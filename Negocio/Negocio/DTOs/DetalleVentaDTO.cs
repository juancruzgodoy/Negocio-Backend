namespace Negocio.DTOs
{
    public class DetalleVentaDTO
    {
        public int Id { get; set; }
        public int IdVenta { get; set; }
        public int IdProducto { get; set; }
        public double Cantidad { get; set; }
        public decimal? PrecioUnitario { get; set; }
        public decimal? Subtotal { get; set; }
    }
}