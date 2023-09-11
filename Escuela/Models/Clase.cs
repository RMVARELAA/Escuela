using System;
using System.Collections.Generic;

namespace Escuela.Models;

public partial class Clase
{
    public int IdClase { get; set; }

    public string NombreClase { get; set; } = null!;

    public string? CodigoClase { get; set; }

    public byte Uv { get; set; }

    public virtual ICollection<Seccion> Seccions { get; set; } = new List<Seccion>();
}
