﻿using System;
using System.Collections.Generic;

namespace Escuela.Models;

public partial class Aula
{
    public int IdAula { get; set; }

    public string? Codigo { get; set; }

    public string NombreAula { get; set; } = null!;

    public virtual ICollection<Clase> Clases { get; set; } = new List<Clase>();
}
