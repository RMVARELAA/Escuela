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
            var nombreclase = _context.Clases.FirstOrDefault(a => a.NombreClase == clase.NombreClase);
            if (nombreclase != null)
            {
                return BadRequest("¡Ya existe una clase con el mismo nombre!");
            }

            ////Verificamos que no se pueda agregar una Clase en la misma aula.
            //var aulaclase = _context.Clases.FirstOrDefault(a => a.IdAula == clase.Id);
            //if (aulaclase != null)
            //{
            //    return BadRequest("¡Ya existe una clase en esta aula!");
            //}

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
