using Escuela.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Escuela.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorariosController : ControllerBase
    {
        private readonly EscuelaContext _context;

        public HorariosController(EscuelaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetHorarios()
        {
            return Ok(await _context.Horarios.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<Horario>>> AddHorario(Horario horario)
        {
            // Convierte las horas ingresadas en formato string a objetos TimeSpan
            TimeSpan horaInicial = TimeSpan.Parse(horario.HoraInicial);
            TimeSpan horaFinal = TimeSpan.Parse(horario.HoraFinal);

            // Verificar si el horario cumple con las restricciones de 8:00 a.m. a 5:00 p.m.
            TimeSpan horaLimiteInicio = TimeSpan.Parse("08:00:00");
            TimeSpan horaLimiteFin = TimeSpan.Parse("17:00:00");

            // Verificar si la hora inicial no es igual a la hora final
            if (horaInicial == horaFinal)
            {
                return BadRequest("La hora inicial no puede ser igual a la hora final.");
            }

            if (horaInicial < horaLimiteInicio || horaFinal > horaLimiteFin)
            {
                return BadRequest("Las clases solo se pueden impartir entre las 8:00 a.m. y las 5:00 p.m.");
            }

            _context.Horarios.Add(horario);
            await _context.SaveChangesAsync();

            return Ok(await _context.Horarios.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Horario>>> UpdateHorario(string horaInicial, string horaFinal, Horario request)
        {
            var dbhorario = await _context.Horarios.FirstOrDefaultAsync(h => h.HoraInicial == horaInicial && h.HoraFinal == horaFinal);

            if (dbhorario == null)
            {
                return NotFound("Horario no encontrado");
            }

            dbhorario.HoraInicial = horaInicial;
            dbhorario.HoraFinal = horaFinal;

            await _context.SaveChangesAsync();

            return Ok(await _context.Horarios.ToListAsync());
        }

        [HttpDelete("{horaInicial}/{horaFinal}")]
        public async Task<ActionResult<List<Horario>>> DeleteHorario(string horaInicial, string horaFinal)
        {
            var horario = await _context.Horarios.FirstOrDefaultAsync(h => h.HoraInicial == horaInicial && h.HoraFinal == horaFinal);

            if (horario == null)
            {
                return NotFound("Horario no encontrado");
            }

            _context.Horarios.Remove(horario);
            await _context.SaveChangesAsync();

            return Ok(await _context.Horarios.ToListAsync());
        }
    }
}
