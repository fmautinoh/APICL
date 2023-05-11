using System;
using System.Collections.Generic;

namespace APICL.Modelos;

public partial class TipoComprobante
{
    public int IdTipComp { get; set; }

    public string TipoComp { get; set; } = null!;

    public virtual ICollection<Comprobante> Comprobantes { get; set; } = new List<Comprobante>();
}
