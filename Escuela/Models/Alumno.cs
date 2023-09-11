using System;
using System.Collections.Generic;

namespace Escuela.Models;

public partial class Alumno
{
    public int IdAlumnos { get; set; }

    public string? Codigo { get; set; }

    public string Nombre { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<MatriculaAlumno> MatriculaAlumnos { get; set; } = new List<MatriculaAlumno>();
}
