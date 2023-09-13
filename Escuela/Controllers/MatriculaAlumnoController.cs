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
            try
            {
                return Ok(await _context.MatriculaAlumnos.ToListAsync());
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
                var idmatriculaAlumno = await _context.MatriculaAlumnos.FindAsync(id);
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
        public async Task<ActionResult<List<MatriculaAlumno>>> AddClaseAlumno(MatriculaAlumno matriculaAlumno)
        {
            try
            {
                // Obtén el número de registros para este alumno.
                int numRegistros = await _context.MatriculaAlumnos
                    .Where(m => m.IdAlumno == matriculaAlumno.IdAlumno)
                    .CountAsync();

                // Verifica si el número de registros es menor o igual a 5.
                if (numRegistros < 5)
                {
                    //// Verifica si el alumno ya está inscrito en alguna sección con la misma hora.
                    //bool horarioClaseExistente = await _context.MatriculaAlumnos
                    //    .Include(m => m.IdSeccionNavigation) // Cargamos la relación con Sección.
                    //    .Where(m => m.IdAlumno == matriculaAlumno.IdAlumno &&
                    //                m.IdSeccionNavigation != null &&
                    //                m.IdSeccionNavigation.HoraInicial == matriculaAlumno.IdSeccionNavigation.HoraInicial)
                    //    .AnyAsync();

                    //if (!horarioClaseExistente)
                    //{
                    //    // Si no hay conflictos de horario ni se ha superado el límite de 5 clases por alumno, procede a guardar los datos en la base de datos.
                    //    _context.MatriculaAlumnos.Add(matriculaAlumno);
                    //    await _context.SaveChangesAsync();
                    //    return Ok(await _context.MatriculaAlumnos.ToListAsync());
                    //}
                    //    _context.MatriculaAlumnos.Add(matriculaAlumno);
                    //    await _context.SaveChangesAsync();
                    //    return Ok(await _context.MatriculaAlumnos.ToListAsync());
                    //else
                    //{
                    //    // Mensaje de error si existe un conflicto de horario.
                    //    return BadRequest("El alumno ya tiene una clase a la misma hora.");
                    //}
                    _context.MatriculaAlumnos.Add(matriculaAlumno);
                    await _context.SaveChangesAsync();
                    return Ok(await _context.MatriculaAlumnos.ToListAsync());
                }
                else
                {
                    // Mensaje de error si se supera el límite de 5 clases por alumno.
                    return BadRequest("¡Se ha alcanzado el límite de 5 Clases para este Alumno!.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult<List<MatriculaAlumno>>> UpdateMatriculaAlumno(MatriculaAlumno request)
        {
            try
            {
                var dbmatriculaAlumno = await _context.MatriculaAlumnos.FindAsync(request.IdMatriculaAlumno);
                if (dbmatriculaAlumno == null)
                    return BadRequest("Clase matriculada no encontrada");

                dbmatriculaAlumno.IdAlumno = request.IdAlumno;
                dbmatriculaAlumno.IdSeccion = request.IdSeccion;

                await _context.SaveChangesAsync();

                return Ok(await _context.MatriculaAlumnos.ToListAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<MatriculaAlumno>>> DeleteMatriculaAlumno(int id)
        {
            try
            {
                var matriculaAlumno = await _context.MatriculaAlumnos.FindAsync(id);
                if (matriculaAlumno == null)
                    return BadRequest("Matricula de alumno no encontrada");

                _context.MatriculaAlumnos.Remove(matriculaAlumno);
                await _context.SaveChangesAsync();

                return Ok(await _context.MatriculaAlumnos.ToListAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
