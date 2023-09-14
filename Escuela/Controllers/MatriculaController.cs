using Escuela.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Collections.Specialized.BitVector32;

namespace Escuela.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatriculaController : ControllerBase
    {
        private readonly EscuelaContext _context;

        public MatriculaController(EscuelaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetMatriculaAlumnos()
        {
            try
            {
                return Ok(await _context.Matriculas.ToListAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetMatriculaAlumnosId(int id)
        {
            try
            {
                var idmatriculaAlumno = await _context.Matriculas.FindAsync(id);
                if (idmatriculaAlumno == null)
                    return BadRequest("Aula no encontrada");
                return Ok(idmatriculaAlumno);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<Matricula>>> AddClaseAlumno(Matricula matricula)
        {
            try
            {
                // Obtén el número de registros para este alumno.
                int numRegistros = await _context.Matriculas
                    .Where(m => m.IdAlumno == matricula.IdAlumno)
                    .CountAsync();

                // Verifica si el número de registros es menor o igual a 5.
                if (numRegistros >= 5)
                {
                    // Mensaje de error si se supera el límite de 5 clases por alumno.
                    return BadRequest("¡Se ha alcanzado el límite de 5 Clases para este Alumno!.");
                }

                // Obtener la sección relacionada con la matrícula
                var dbseccion = await _context.Seccions.FindAsync(matricula.IdSeccion);

                if (dbseccion == null)
                {
                    return BadRequest("La sección no existe en la base de datos.");
                }

                // Verificar si la clase ya está matriculada por el mismo alumno
                var matriculaExistente = await _context.Matriculas
                    .FirstOrDefaultAsync(m => m.IdAlumno == matricula.IdAlumno && m.IdSeccion == matricula.IdSeccion);

                if (matriculaExistente != null)
                {
                    return BadRequest("El alumno ya está matriculado en esta clase.");
                }

                // Obtener la sección relacionada con la matrícula
                var seccion = await _context.Seccions.FindAsync(matricula.IdSeccion);

                if (seccion == null)
                {
                    return BadRequest("La sección no existe en la base de datos.");
                }

                // Verificar si el alumno tiene una o más veces matriculada la misma clase (aunque sean en diferentes horarios)
                var dbclase = await _context.Matriculas
                    .AnyAsync(m => m.IdAlumno == matricula.IdAlumno && m.IdSeccionNavigation != null &&
                    m.IdSeccionNavigation.IdClase != null && m.IdSeccionNavigation.IdClase == seccion.IdClase);

                if (dbclase)
                {
                    return BadRequest("El alumno ya está matriculado en una sección de esta clase.");
                }

                // Verificar si el alumno ya está matriculado en una sección con la misma hora inicial
                var seccionesConMismaHora = await _context.Matriculas
                    .Include(m => m.IdSeccionNavigation)
                    .Where(m => m.IdAlumno == matricula.IdAlumno && m.IdSeccionNavigation != null &&
                        m.IdSeccionNavigation.HoraInicial == seccion.HoraInicial)
                    .ToListAsync();

                if (seccionesConMismaHora.Any())
                {
                    return BadRequest("El alumno ya está matriculado en una sección con la misma hora.");
                }
                else
                {
                    _context.Matriculas.Add(matricula);
                    await _context.SaveChangesAsync();
                    return Ok(await _context.Matriculas.ToListAsync());
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult<List<Matricula>>> UpdateMatriculaAlumno(Matricula request)
        {
            try
            {
                var dbmatricula = await _context.Matriculas.FindAsync(request.IdMatricula);
                if (dbmatricula == null)
                    return BadRequest("Clase matriculada no encontrada");

                // Obtén el número de registros para este alumno.
                int numRegistros = await _context.Matriculas
                    .Where(m => m.IdAlumno == request.IdAlumno)
                    .CountAsync();

                // Verifica si el número de registros es menor o igual a 5.
                if (numRegistros >= 5)
                {
                    // Mensaje de error si se supera el límite de 5 clases por alumno.
                    return BadRequest("¡Se ha alcanzado el límite de 5 Clases para este Alumno!.");
                }

                // Obtener la sección relacionada con la matrícula
                var dbseccion = await _context.Seccions.FindAsync(request.IdSeccion);

                if (dbseccion == null)
                {
                    return BadRequest("La sección no existe en la base de datos.");
                }

                // Verificar si la clase ya está matriculada por el mismo alumno
                var matriculaExistente = await _context.Matriculas
                    .FirstOrDefaultAsync(m => m.IdAlumno == request.IdAlumno && m.IdSeccion == request.IdSeccion);

                if (matriculaExistente != null)
                {
                    return BadRequest("El alumno ya está matriculado en esta clase.");
                }

                // Obtener la sección relacionada con la matrícula
                var seccion = await _context.Seccions.FindAsync(request.IdSeccion);

                if (seccion == null)
                {
                    return BadRequest("La sección no existe en la base de datos.");
                }

                // Verificar si el alumno tiene una o más veces matriculada la misma clase (aunque sean en diferentes horarios)
                var dbclase = await _context.Matriculas
                    .AnyAsync(m => m.IdAlumno == request.IdAlumno && m.IdSeccionNavigation != null &&
                    m.IdSeccionNavigation.IdClase != null && m.IdSeccionNavigation.IdClase == seccion.IdClase);

                if (dbclase)
                {
                    return BadRequest("El alumno ya está matriculado en una sección de esta clase.");
                }

                // Verificar si el alumno ya está matriculado en una sección con la misma hora inicial
                var seccionesConMismaHora = await _context.Matriculas
                    .Include(m => m.IdSeccionNavigation)
                    .Where(m => m.IdAlumno == request.IdAlumno && m.IdSeccionNavigation != null &&
                        m.IdSeccionNavigation.HoraInicial == seccion.HoraInicial)
                    .ToListAsync();

                if (seccionesConMismaHora.Any())
                {
                    return BadRequest("El alumno ya está matriculado en una sección con la misma hora.");
                }

                dbmatricula.IdAlumno = request.IdAlumno;
                dbmatricula.IdSeccion = request.IdSeccion;

                await _context.SaveChangesAsync();

                return Ok(await _context.Matriculas.ToListAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Matricula>>> DeleteMatriculaAlumno(int id)
        {
            try
            {
                var matriculaAlumno = await _context.Matriculas.FindAsync(id);
                if (matriculaAlumno == null)
                    return BadRequest("Matricula de alumno no encontrada");

                _context.Matriculas.Remove(matriculaAlumno);
                await _context.SaveChangesAsync();

                return Ok(await _context.Matriculas.ToListAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}