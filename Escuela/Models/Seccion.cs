using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Escuela.Models;

public partial class Seccion
{
    public int IdSeccion { get; set; }

    public int? IdMaestro { get; set; }

    public int? IdClase { get; set; }

    public int? IdAula { get; set; }

    public TimeSpan HoraInicial { get; set; }

    public TimeSpan HoraFinal { get; set; }

    [JsonIgnore]
    public virtual Aula? IdAulaNavigation { get; set; }

    [JsonIgnore]
    public virtual Clase? IdClaseNavigation { get; set; }

    [JsonIgnore]
    public virtual Maestro? IdMaestroNavigation { get; set; }

    [JsonIgnore]
    public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
}
