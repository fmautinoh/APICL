using System;
using System.Collections.Generic;

namespace APICL.Modelos;

public partial class TipoProducto
{
    public int IdTipPro { get; set; }

    public string TipoPro { get; set; } = null!;

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
