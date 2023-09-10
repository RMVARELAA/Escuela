using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Escuela.Models;

public partial class MaestrosClase
{
    public int IdClaseMaestro { get; set; }

    public int IdMaestros { get; set; }

    public int IdClase { get; set; }

    [JsonIgnore]
    public virtual Clase IdClaseNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual Maestro IdMaestrosNavigation { get; set; } = null!;
}
