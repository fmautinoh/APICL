using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace APICL.Modelos;

public partial class Producto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdProd { get; set; }

    public string NomProd { get; set; } = null!;

    public int IdTipPro { get; set; }

    public decimal PrecioProd { get; set; }

    public int DurProd { get; set; }

    [JsonIgnore]
    public virtual TipoProducto IdTipProNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();
}
