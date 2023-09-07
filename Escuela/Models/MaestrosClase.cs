using System;
using System.Collections.Generic;

namespace Escuela.Models;

public partial class MaestrosClase
{
    public int? IdMaestros { get; set; }

    public int? IdClase { get; set; }

    public int? IdAula { get; set; }

    public virtual Aula? IdAulaNavigation { get; set; }

    public virtual Clase? IdClaseNavigation { get; set; }

    public virtual Maestro? IdMaestrosNavigation { get; set; }
}
