using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Escuela.Models;

public partial class MaestrosClase
{
    public int IdClaseMaestro { get; set; }

    public int? IdMaestros { get; set; }

    public int? IdSeccion { get; set; }

    [JsonIgnore]
    public virtual Maestro? IdMaestrosNavigation { get; set; }

    [JsonIgnore]
    public virtual Seccion? IdSeccionNavigation { get; set; }
}