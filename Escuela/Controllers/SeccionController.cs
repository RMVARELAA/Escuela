using Escuela.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Escuela.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeccionController : ControllerBase
    {
        private readonly EscuelaContext _context;

        public SeccionController(EscuelaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetSeccion()
        {
            try
            {
                return Ok(await _context.Seccions.ToListAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<Seccion>>> AddSeccion(Seccion seccion)
        {
            try
            {
                //Verificamos que no se pueda agregar un aula con el mismo nombre.
                //var nombreaula = _context.Aulas.FirstOrDefault(a => a.NombreAula == aula.NombreAula);
                //if (nombreaula != null)
                //{
                //    return BadRequest("¡Ya existe un aula con el mismo nombre!");
                //}

                //Si no hay datos duplicados se procede a guardar los datos en la base de datos.
                _context.Seccions.Add(seccion);
                await _context.SaveChangesAsync();

                return Ok(await _context.Seccions.ToListAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
