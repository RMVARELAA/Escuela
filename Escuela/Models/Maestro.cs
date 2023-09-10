using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Escuela.Models;

public partial class Maestro
{
    public int IdMaestros { get; set; }

    public string? Codigo { get; set; }

    public string Nombre { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Email { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<MaestrosClase> MaestrosClases { get; set; } = new List<MaestrosClase>();
}
