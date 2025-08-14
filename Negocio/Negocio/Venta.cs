using System;
using System.Collections.Generic;

namespace Negocio;
public partial class Venta
{
    public int Id { get; set; }

    public int IdCliente { get; set; }

    public DateTime Fecha { get; set; }

    public decimal? TotalVenta { get; set; }

    public string? MetodoDePago { get; set; }

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual Cliente IdClienteNavigation { get; set; } = null!;
}
