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

            ////Verificamos que no se pueda agregar un Maestro con el mismo telefono.
            //var telefonomaestro = _context.Alumnos.FirstOrDefault(a => a.Telefono == alumno.Telefono);
            //if (telefonomaestro != null)
            //{
            //    return BadRequest("¡Ya existe un alumno con el mismo telefono!");
            //}

            ////Verificamos que no se pueda agregar un Maestro con el mismo correo.
            //var emailmaestro = _context.Alumnos.FirstOrDefault(a => a.Email == alumno.Email);
            //if (emailmaestro != null)
            //{
            //    return BadRequest("¡Ya existe un alumno con el mismo correo!");
            //}

            //Si no hay datos duplicados se procede a guardar los datos en la base de datos.
            _context.Clases.Add(clase);
            await _context.SaveChangesAsync();

            return Ok(await _context.Clases.ToListAsync());
        }
    }
}
