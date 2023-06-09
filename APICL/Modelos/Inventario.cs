﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace APICL.Modelos;

public partial class Inventario
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdInv { get; set; }

    public int IdProd { get; set; }

    public DateTime FechProduc { get; set; }

    public int StokPro { get; set; }

    public int IdPresent { get; set; }

    public int EstadoInv { get; set; }
    [JsonIgnore]
    public virtual Presentacion IdPresentNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual Producto IdProdNavigation { get; set; } = null!;
}
