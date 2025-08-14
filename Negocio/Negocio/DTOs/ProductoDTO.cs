namespace Negocio.DTOs
{
    public class ProductoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? UnidadDeMedida { get; set; }
        public decimal? PrecioVenta { get; set; }
        public double? StockActual { get; set; }
        public int IdCategoria { get; set; }
        public int IdProveedor { get; set; }
    }
}