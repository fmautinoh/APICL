using System;
using System.Collections.Generic;

namespace APICL.Modelos;

public partial class Comprobante
{
    public int IdComp { get; set; }

    public string NumeComp { get; set; } = null!;

    public decimal Igv { get; set; }

    public int IdTipComp { get; set; }

    public virtual TipoComprobante IdTipCompNavigation { get; set; } = null!;

    public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();
}
