using System;
using System.Collections.Generic;

namespace Negocio;
public partial class Proveedor
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Tipo { get; set; }

    public string? Telefono { get; set; }

    public string? Cuit { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
