using System;
using System.Collections.Generic;

namespace Negocio;
public partial class VistaProducto
{
    public string Nombre { get; set; } = null!;

    public string? UnidadDeMedida { get; set; }

    public decimal? PrecioVenta { get; set; }

    public double? StockActual { get; set; }

    public string Categoria { get; set; } = null!;

    public string Proveedor { get; set; } = null!;
}
