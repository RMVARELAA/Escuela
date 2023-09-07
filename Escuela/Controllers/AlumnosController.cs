using Escuela.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Escuela.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnosController : ControllerBase
    {
        private readonly EscuelaContext _context;

        public AlumnosController(EscuelaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAlumnos()
        {
            return Ok(await _context.Alumnos.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetAlumnosid(int id)
        {
            var alumno = await _context.Alumnos.FindAsync(id);
            if (alumno == null)
                return BadRequest("Alumno no encontrado");
            return Ok(alumno);
        }

        [HttpPost]
        public async Task<ActionResult<List<Alumno>>> AddAlumno(Alumno alumno)
        {
            //Verificamos que no se pueda agregar un Maestro con el mismo nombre.
            var nombrealumno = _context.Alumnos.FirstOrDefault(a => a.Nombre == alumno.Nombre);
            if (nombrealumno != null)
            {
                return BadRequest("¡Ya existe un alumno con el mismo nombre!");
            }

            //Verificamos que no se pueda agregar un Maestro con el mismo telefono.
            var telefonomaestro = _context.Alumnos.FirstOrDefault(a => a.Telefono == alumno.Telefono);
            if (telefonomaestro != null)
            {
                return BadRequest("¡Ya existe un alumno con el mismo telefono!");
            }

            //Verificamos que no se pueda agregar un Maestro con el mismo correo.
            var emailmaestro = _context.Alumnos.FirstOrDefault(a => a.Email == alumno.Email);
            if (emailmaestro != null)
            {
                return BadRequest("¡Ya existe un alumno con el mismo correo!");
            }

            //Si no hay datos duplicados se procede a guardar los datos en la base de datos.
            _context.Alumnos.Add(alumno);
            await _context.SaveChangesAsync();

            return Ok(await _context.Alumnos.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Alumno>>> UpdateAlumnos(Alumno request)
        {
            var dbalumno = await _context.Alumnos.FindAsync(request.IdAlumnos);
            if (dbalumno == null)
                return BadRequest("Alumno no encontrado");

            dbalumno.Nombre = request.Nombre;
            dbalumno.Telefono = request.Telefono;
            dbalumno.Email = request.Email;

            await _context.SaveChangesAsync();

            return Ok(await _context.Alumnos.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Alumno>>> Delete(int id)
        {
            var dbalumno = await _context.Alumnos.FindAsync(id);
            if (dbalumno == null)
                return BadRequest("Alumno no encontrado");

            _context.Alumnos.Remove(dbalumno);
            await _context.SaveChangesAsync();

            return Ok(await _context.Alumnos.ToListAsync());
        }
    }
}
