using System;
using System.Collections.Generic;

namespace Escuela.Models;

public partial class MaestrosClase
{
    public int IdClaseMaestro { get; set; }

    public int IdMaestros { get; set; }

    public int IdSeccion { get; set; }

    public virtual Maestro IdMaestrosNavigation { get; set; } = null!;

    public virtual Seccion IdSeccionNavigation { get; set; } = null!;
}
