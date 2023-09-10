using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Escuela.Models;

public partial class MatriculaAlumno
{
    public int IdMatriculaAlumno { get; set; }

    public int IdAlumno { get; set; }

    public int IdClase { get; set; }

    [JsonIgnore]
    public virtual Alumno IdAlumnoNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual Clase IdClaseNavigation { get; set; } = null!;
}
