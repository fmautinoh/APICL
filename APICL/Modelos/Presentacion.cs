using System;
using System.Collections.Generic;

namespace APICL.Modelos;

public partial class Presentacion
{
    public int IdPres { get; set; }

    public string TipPres { get; set; } = null!;

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();
}
