using System;
using System.Collections.Generic;

namespace APICL.Modelos;

public partial class Producto
{
    public int IdProd { get; set; }

    public string NomProd { get; set; } = null!;

    public int IdTipPro { get; set; }

    public decimal PrecioProd { get; set; }

    public int DurProd { get; set; }

    public virtual TipoProducto IdTipProNavigation { get; set; } = null!;

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();
}
