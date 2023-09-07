using System;
using System.Collections.Generic;

namespace Escuela.Models;

public partial class MatriculaAlumno
{
    public int? IdAlumno { get; set; }

    public int? IdClase { get; set; }

    public int? IdAula { get; set; }

    public virtual Alumno? IdAlumnoNavigation { get; set; }

    public virtual Aula? IdAulaNavigation { get; set; }

    public virtual Clase? IdClaseNavigation { get; set; }
}
