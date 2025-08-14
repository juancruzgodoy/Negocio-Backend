using System;
using System.Collections.Generic;

namespace Negocio;
public partial class DetalleVenta
{
    public int Id { get; set; }

    public int? IdVenta { get; set; }

    public int? IdProducto { get; set; }

    public double? Cantidad { get; set; }

    public decimal? PrecioUnitario { get; set; }

    public decimal? Subtotal { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }

    public virtual Venta? IdVentaNavigation { get; set; }
}
