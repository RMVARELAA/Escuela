using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Escuela.Models;

public partial class Clase
{
    public int IdClase { get; set; }

    public string Clase1 { get; set; } = null!;

    public string Uv { get; set; } = null!;

    public string HoraInicial { get; set; } = null!;

    public string HoraFinal { get; set; } = null!;

    public int? AulaId { get; set; }

    [JsonIgnore]
    public virtual Aula? Aula { get; set; }

    [JsonIgnore]
    public virtual Horario Hora { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<MaestrosClase> MaestrosClases { get; set; } = new List<MaestrosClase>();

    [JsonIgnore]
    public virtual ICollection<MatriculaAlumno> MatriculaAlumnos { get; set; } = new List<MatriculaAlumno>();
}
