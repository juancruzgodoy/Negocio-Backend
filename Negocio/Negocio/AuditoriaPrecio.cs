using System;
using System.Collections.Generic;

namespace Negocio;
public partial class AuditoriaPrecio
{
    public int? IdProducto { get; set; }

    public decimal? PrecioAnterior { get; set; }

    public decimal? PrecioNuevo { get; set; }

    public DateTime? FechaModificacion { get; set; }
}
