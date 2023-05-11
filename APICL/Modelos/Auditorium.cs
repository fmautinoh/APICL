using System;
using System.Collections.Generic;

namespace APICL.Modelos;

public partial class Auditorium
{
    public int Id { get; set; }

    public string TablaAfectada { get; set; } = null!;

    public string Accion { get; set; } = null!;

    public DateTime Fecha { get; set; }

    public string Usuario { get; set; } = null!;

    public string? Detalles { get; set; }
}
