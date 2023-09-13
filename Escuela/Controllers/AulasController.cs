using Azure.Core;
using Escuela.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Escuela.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AulasController : ControllerBase
    {
        private readonly EscuelaContext _context;

        public AulasController(EscuelaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAulas()
        {
            try
            {
                return Ok(await _context.Aulas.ToListAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetAulasid(int id)
        {
            try
            {
                var aula = await _context.Aulas.FindAsync(id);
                if (aula == null)
                    return BadRequest("Aula no encontrada");
                return Ok(aula);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<Aula>>> AddAula(Aula aula)
        {
            try
            {
                //Verificamos que no se pueda agregar un aula con el mismo nombre.
                var nombreaula = _context.Aulas.FirstOrDefault(a => a.NombreAula == aula.NombreAula);
                if (nombreaula != null)
                {
                    return BadRequest("¡Ya existe un aula con el mismo nombre!");
                }

                //Si no hay datos duplicados se procede a guardar los datos en la base de datos.
                _context.Aulas.Add(aula);
                await _context.SaveChangesAsync();

                return Ok(await _context.Aulas.ToListAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult<List<Aula>>> UpdateAula(Aula request)
        {
            try
            {
                var dbaula = await _context.Aulas.FindAsync(request.IdAula);
                if (dbaula == null)
                    return BadRequest("Aula no encontrada");

                dbaula.NombreAula = request.NombreAula;

                await _context.SaveChangesAsync();

                return Ok(await _context.Aulas.ToListAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Aula>>> Delete(int id)
        {
            try
            {
                var dbaula = await _context.Aulas.FindAsync(id);
                if (dbaula == null)
                    return BadRequest("Aula no encontrada");

                _context.Aulas.Remove(dbaula);
                await _context.SaveChangesAsync();

                return Ok(await _context.Aulas.ToListAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
