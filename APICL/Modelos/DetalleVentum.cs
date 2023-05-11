using System;
using System.Collections.Generic;

namespace APICL.Modelos;

public partial class DetalleVentum
{
    public int IdVent { get; set; }

    public int IdInv { get; set; }

    public int CanVent { get; set; }

    public virtual Inventario IdInvNavigation { get; set; } = null!;

    public virtual Ventum IdVentNavigation { get; set; } = null!;
}
