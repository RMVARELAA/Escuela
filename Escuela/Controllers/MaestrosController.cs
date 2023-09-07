using Escuela.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Escuela.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaestrosController : ControllerBase
    {
        private readonly EscuelaContext _context;

        public MaestrosController(EscuelaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetMaestros()
        {
            return Ok(await _context.Maestros.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetMaestrosid(int id)
        {
            var maestro = await _context.Maestros.FindAsync(id);
            if (maestro == null)
                return BadRequest("Maestro no encontrado");
            return Ok(maestro);
        }

        [HttpPost]
        public async Task<ActionResult<List<Maestro>>> AddMaestro(Maestro maestro)
        {
            //Verificamos que no se pueda agregar un Maestro con el mismo nombre.
            var nombremaestro = _context.Maestros.FirstOrDefault(a => a.Nombre == maestro.Nombre);
            if (nombremaestro != null)
            {
                return BadRequest("¡Ya existe un maestro con el mismo nombre!");
            }

            //Verificamos que no se pueda agregar un Maestro con el mismo telefono.
            var telefonomaestro = _context.Maestros.FirstOrDefault(a => a.Telefono == maestro.Telefono);
            if (telefonomaestro != null)
            {
                return BadRequest("¡Ya existe un maestro con el mismo telefono!");
            }

            //Verificamos que no se pueda agregar un Maestro con el mismo correo.
            var emailmaestro = _context.Maestros.FirstOrDefault(a => a.Email == maestro.Email);
            if (emailmaestro != null)
            {
                return BadRequest("¡Ya existe un maestro con el mismo correo!");
            }

            //Si no hay datos duplicados se procede a guardar los datos en la base de datos.
            _context.Maestros.Add(maestro);
            await _context.SaveChangesAsync();

            return Ok(await _context.Maestros.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Maestro>>> UpdateMaestros(Maestro request)
        {
            var dbmaestro = await _context.Maestros.FindAsync(request.IdMaestros);
            if (dbmaestro == null)
                return BadRequest("Maestro no encontrado");

            dbmaestro.Nombre = request.Nombre;
            dbmaestro.Telefono = request.Telefono;
            dbmaestro.Email = request.Email;

            await _context.SaveChangesAsync();

            return Ok(await _context.Maestros.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Maestro>>> Delete(int id)
        {
            var dbmaestro = await _context.Maestros.FindAsync(id);
            if (dbmaestro == null)
                return BadRequest("Maestro no encontrado");

            _context.Maestros.Remove(dbmaestro);
            await _context.SaveChangesAsync();

            return Ok(await _context.Maestros.ToListAsync());
        }
    }
}
