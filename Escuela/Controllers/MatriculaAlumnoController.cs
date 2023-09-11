using Escuela.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Escuela.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatriculaAlumnoController : ControllerBase
    {
        private readonly EscuelaContext _context;

        public MatriculaAlumnoController(EscuelaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetMatriculaAlumnos()
        {
            return Ok(await _context.MatriculaAlumnos.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<MatriculaAlumno>>> AddClaseAlumno(MatriculaAlumno matriculaAlumno)
        {
            int numRegistros = await _context.MatriculaAlumnos
                .Where(m => m.IdAlumno == matriculaAlumno.IdAlumno)
                .CountAsync();

            // Verifica si el número de registros es menor o igual a 5.
            if (numRegistros < 5)
            {
                // Si no hay datos duplicados se procede a guardar los datos en la base de datos.
                _context.MatriculaAlumnos.Add(matriculaAlumno);
                await _context.SaveChangesAsync();

                return Ok(await _context.MatriculaAlumnos.ToListAsync());
            }
            else
            {
                // Muestra un mensaje de error si se alcanza el límite de 5 IdSeccion.
                return BadRequest("¡Se ha alcanzado el límite de 5 Clases para este Alumno!.");
            }
        }

    }
}
