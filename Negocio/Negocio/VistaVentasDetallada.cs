using System;
using System.Collections.Generic;

namespace Negocio;
public partial class VistaVentasDetallada
{
    public int IdVenta { get; set; }

    public DateTime Fecha { get; set; }

    public string? MetodoDePago { get; set; }

    public string Producto { get; set; } = null!;

    public string CategoriaProducto { get; set; } = null!;

    public double? Cantidad { get; set; }

    public string? UnidadDeMedida { get; set; }

    public decimal? PrecioUnitario { get; set; }

    public decimal? Subtotal { get; set; }
}
