using System;
using System.Collections.Generic;

namespace Negocio;
public partial class Producto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int? IdCategoria { get; set; }

    public int? IdProveedor { get; set; }

    public string? UnidadDeMedida { get; set; }

    public decimal? PrecioVenta { get; set; }

    public double? StockActual { get; set; }

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual Categoria? IdCategoriaNavigation { get; set; }

    public virtual Proveedor? IdProveedorNavigation { get; set; }
}
