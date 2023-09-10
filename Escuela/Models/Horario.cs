using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Escuela.Models;

public partial class Horario
{
    public string HoraInicial { get; set; } = null!;

    public string HoraFinal { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Clase> Clases { get; set; } = new List<Clase>();
}
