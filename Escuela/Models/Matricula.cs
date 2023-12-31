﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Escuela.Models;

public partial class Matricula
{
    public int IdMatricula { get; set; }

    public int? IdAlumno { get; set; }

    public int? IdSeccion { get; set; }

    [JsonIgnore]
    public virtual Alumno? IdAlumnoNavigation { get; set; }

    [JsonIgnore]
    public virtual Seccion? IdSeccionNavigation { get; set; }
}
