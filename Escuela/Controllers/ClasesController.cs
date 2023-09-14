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
            try
            {
                return Ok(await _context.Clases.ToListAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetClasesId(int id)
        {
            try
            {
                var clase = await _context.Clases.FindAsync(id);
                if (clase == null)
                    return BadRequest("Clase no encontrada");
                return Ok(clase);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<Clase>>> AddClase(Clase clase)
        {
            try
            {
                //Verificamos que no se pueda agregar una Clase con el mismo nombre.
                var nombreclase = _context.Clases.FirstOrDefault(a => a.NombreClase == clase.NombreClase);
                if (nombreclase != null)
                {
                    return BadRequest("¡Ya existe una clase con el mismo nombre!");
                }

                //Si no hay datos duplicados se procede a guardar los datos en la base de datos.
                _context.Clases.Add(clase);
                await _context.SaveChangesAsync();

                return Ok(await _context.Clases.ToListAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult<List<Clase>>> UpdateClase(Clase request)
        {
            try
            {
                var dbclase = await _context.Clases.FindAsync(request.IdClase);
                if (dbclase == null)
                    return BadRequest("Clase no encontrada");

                //Verificamos que no se pueda agregar una Clase con el mismo nombre.
                var nombreclase = _context.Clases.FirstOrDefault(a => a.NombreClase == request.NombreClase);
                if (nombreclase != null)
                {
                    return BadRequest("¡Ya existe una clase con el mismo nombre!");
                }

                dbclase.NombreClase = request.NombreClase;
                dbclase.Uv = request.Uv;

                await _context.SaveChangesAsync();

                return Ok(await _context.Clases.ToListAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Clase>>> DeleteClase(int id)
        {
            try
            {
                var dbclase = await _context.Clases.FindAsync(id);
                if (dbclase == null)
                    return BadRequest("Clase no encontrado");

                _context.Clases.Remove(dbclase);
                await _context.SaveChangesAsync();

                return Ok(await _context.Clases.ToListAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
