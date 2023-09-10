using Escuela.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Escuela.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClasesController : ControllerBase
    {
        private readonly EscuelaContext _context;

        public ClasesController(EscuelaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetClases()
        {
            return Ok(await _context.Clases.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetClasesId(int id)
        {
            var clase = await _context.Clases.FindAsync(id);
            if (clase == null)
                return BadRequest("Clase no encontrada");
            return Ok(clase);
        }

        [HttpPost]
        public async Task<ActionResult<List<Clase>>> AddClase(Clase clase)
        {
            //Verificamos que no se pueda agregar una Clase con el mismo nombre.
            var nombreclase = _context.Clases.FirstOrDefault(a => a.Clase1 == clase.Clase1);
            if (nombreclase != null)
            {
                return BadRequest("¡Ya existe una clase con el mismo nombre!");
            }

            //Verificamos que no se pueda agregar una Clase en la misma aula.
            var aulaclase = _context.Clases.FirstOrDefault(a => a.AulaId == clase.AulaId);
            if (aulaclase != null)
            {
                return BadRequest("¡Ya existe una clase en esta aula!");
            }

            // Convierte las horas ingresadas en formato string a objetos TimeSpan
            TimeSpan horaInicial = TimeSpan.Parse(clase.HoraInicial);
            TimeSpan horaFinal = TimeSpan.Parse(clase.HoraFinal);

            // Verificar si ya existe una clase en la misma aula y en el mismo horario
            var aulaclases = await _context.Clases.FirstOrDefaultAsync(a =>
                a.AulaId == clase.AulaId &&
                ((TimeSpan.Parse(a.HoraInicial) <= horaInicial && horaInicial < TimeSpan.Parse(a.HoraFinal)) ||
                 (TimeSpan.Parse(a.HoraInicial) < horaFinal && horaFinal <= TimeSpan.Parse(a.HoraFinal)) ||
                 (horaInicial <= TimeSpan.Parse(a.HoraInicial) && TimeSpan.Parse(a.HoraFinal) <= horaFinal)));

            if (aulaclase != null)
            {
                return BadRequest("¡Ya existe una clase en esta aula y en el mismo horario!");
            }

            //Si no hay datos duplicados se procede a guardar los datos en la base de datos.
            _context.Clases.Add(clase);
            await _context.SaveChangesAsync();

            return Ok(await _context.Clases.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Clase>>> DeleteClase(int id)
        {
            var dbclase = await _context.Clases.FindAsync(id);
            if (dbclase == null)
                return BadRequest("Clase no encontrado");

            _context.Clases.Remove(dbclase);
            await _context.SaveChangesAsync();

            return Ok(await _context.Clases.ToListAsync());
        }
    }
}
