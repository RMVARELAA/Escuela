using System;
using System.Collections.Generic;

namespace Escuela.Models;

public partial class Seccion
{
    public int IdSeccion { get; set; }

    public int IdClase { get; set; }

    public int IdAula { get; set; }

    public TimeSpan HoraInicial { get; set; }

    public TimeSpan HoraFinal { get; set; }

    public virtual Aula IdAulaNavigation { get; set; } = null!;

    public virtual Clase IdClaseNavigation { get; set; } = null!;

    public virtual ICollection<MaestrosClase> MaestrosClases { get; set; } = new List<MaestrosClase>();

    public virtual ICollection<MatriculaAlumno> MatriculaAlumnos { get; set; } = new List<MatriculaAlumno>();
}
