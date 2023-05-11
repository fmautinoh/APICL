using System;
using System.Collections.Generic;

namespace APICL.Modelos;

public partial class Inventario
{
    public int IdInv { get; set; }

    public int IdProd { get; set; }

    public DateTime FechProduc { get; set; }

    public int StokPro { get; set; }

    public int IdPresent { get; set; }

    public int EstadoInv { get; set; }

    public virtual Presentacion IdPresentNavigation { get; set; } = null!;

    public virtual Producto IdProdNavigation { get; set; } = null!;
}
