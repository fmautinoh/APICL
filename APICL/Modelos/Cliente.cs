using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APICL.Modelos;

public partial class Cliente
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdCli { get; set; }

    public string DniCli { get; set; } = null!;

    public string? RucCli { get; set; }

    public string NomCli { get; set; } = null!;

    public string ApatCli { get; set; } = null!;

    public string AmatCli { get; set; } = null!;

    public string TelCli { get; set; } = null!;

    public string CorCli { get; set; } = null!;

    public string DirCli { get; set; } = null!;
}
