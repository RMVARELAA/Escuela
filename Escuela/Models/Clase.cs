using System;
using System.Collections.Generic;

namespace Escuela.Models;

public partial class Clase
{
    public int IdClase { get; set; }

    public string Clase1 { get; set; } = null!;

    public string Uv { get; set; } = null!;

    public string HoraInicial { get; set; } = null!;

    public string HoraFinal { get; set; } = null!;

    public int? AulaId { get; set; }

    public virtual Aula? Aula { get; set; }

    public virtual Horario Hora { get; set; } = null!;
}
