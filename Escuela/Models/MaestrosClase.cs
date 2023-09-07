using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Escuela.Models;

public partial class MaestrosClase
{
    public int? IdMaestros { get; set; }

    public int? IdClase { get; set; }

    public int? IdAula { get; set; }

    [JsonIgnore]
    public virtual Aula? IdAulaNavigation { get; set; }

    [JsonIgnore]

    public virtual Clase? IdClaseNavigation { get; set; }

    [JsonIgnore]
    public virtual Maestro? IdMaestrosNavigation { get; set; }
}
