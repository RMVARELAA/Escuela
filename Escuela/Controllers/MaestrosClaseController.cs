using Escuela.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Escuela.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaestrosClaseController : ControllerBase
    {
        private readonly EscuelaContext _context;

        public MaestrosClaseController(EscuelaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetMaestrosClase()
        {
            try
            {
                return Ok(await _context.MaestrosClases.ToListAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<MatriculaAlumno>>> AddClaseAlumno(MaestrosClase maestrosClase)
        {
            try
            {
                // Obtén el número de registros para este alumno.
                int clasesMaestros = await _context.MaestrosClases
                    .Where(m => m.IdMaestros == maestrosClase.IdMaestros)
                    .CountAsync();

                // Verifica si el número de registros es menor o igual a 5.
                if (clasesMaestros < 5)
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
                    _context.MaestrosClases.Add(maestrosClase);
                    await _context.SaveChangesAsync();
                    return Ok(await _context.MaestrosClases.ToListAsync());
                }
                else
                {
                    // Mensaje de error si se supera el límite de 5 clases por alumno.
                    return BadRequest("¡Se ha alcanzado el límite de 4 Clases para este Maestro!.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
