using System;
using System.Collections.Generic;

namespace Negocio;
public partial class Cliente
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Telefono { get; set; }

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
