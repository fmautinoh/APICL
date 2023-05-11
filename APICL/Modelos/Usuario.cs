using System;
using System.Collections.Generic;

namespace APICL.Modelos;

public partial class Usuario
{
    public int IdUsu { get; set; }

    public string Username { get; set; } = null!;

    public string Pasw { get; set; } = null!;

    public string? TipoUsuario { get; set; }
}
