using System;
using System.Collections.Generic;

namespace APICL.Modelos;

public partial class Ventum
{
    public int IdVent { get; set; }

    public int IdCli { get; set; }

    public DateTime FechVent { get; set; }

    public decimal TotPre { get; set; }

    public int IdComp { get; set; }

    public virtual Comprobante IdCompNavigation { get; set; } = null!;
}
