using Azure.Core;
using Escuela.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

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

        [HttpGet("{id}")]
        public async Task<ActionResult> GetSeccionId(int id)
        {
            try
            {
                var idseccion = await _context.Seccions.FindAsync(id);
                if (idseccion == null)
                    return BadRequest("Aula no encontrada");
                return Ok(idseccion);
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
                // Verificar que el maestro no tenga más de 4 clases
                var maestro = await _context.Maestros
                    .Include(m => m.Seccions)
                    .FirstOrDefaultAsync(m => m.IdMaestros == seccion.IdMaestro);

                //Verificamos que el maestro no tenga más de 4 clases asignadas
                if (maestro != null && maestro.Seccions.Count >= 4)
                {
                    return BadRequest("El maestro ya tiene 4 clases asignadas.");
                }

                // Verificamos que la hora inicial esté entre las 8 am y las 4 pm
                if (seccion.HoraInicial < TimeSpan.FromHours(8) || seccion.HoraInicial > TimeSpan.FromHours(16))
                {
                    return BadRequest("La hora inicial debe estar entre las 8:00 a.m. y las 4:00 p.m.");
                }

                // Verificamos que la hora final esté entre las 9 am y las 5 pm
                if (seccion.HoraFinal < TimeSpan.FromHours(9) || seccion.HoraFinal > TimeSpan.FromHours(17))
                {
                    return BadRequest("La hora final debe estar entre las 9:00 a.m. y las 5:00 p.m.");
                }

                // Verificamos que la hora inicial y final no sean iguales
                if (seccion.HoraInicial == seccion.HoraFinal)
                {
                    return BadRequest("La hora inicial y final no pueden ser iguales.");
                }

                // Verificamos que la hora inicial no sea mayor que la hora final
                if (seccion.HoraInicial > seccion.HoraFinal)
                {
                    return BadRequest("La hora inicial no puede ser mayor que la hora final.");
                }

                // Verificamos que no haya otra sección en el mismo aula en la misma hora inicial
                var seccionExistente = await _context.Seccions
                    .FirstOrDefaultAsync(s => s.IdAula == seccion.IdAula && s.HoraInicial == seccion.HoraInicial);

                if (seccionExistente != null)
                {
                    return BadRequest("Ya existe una sección en el mismo aula en la misma hora inicial.");
                }

                // Verificar que el maestro no tenga clases a la misma hora
                var HorariosSeccion = await _context.Seccions
                    .Where(s => s.IdMaestro == seccion.IdMaestro && s.HoraInicial == seccion.HoraInicial)
                    .ToListAsync();

                if (HorariosSeccion.Any())
                {
                    return BadRequest("El maestro ya tiene una clase en el mismo horario.");
                }

                // Si todas las validaciones pasan, agregar la sección
                _context.Seccions.Add(seccion);
                await _context.SaveChangesAsync();

                return Ok(await _context.Seccions.ToListAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult<List<Seccion>>> UpdateSeccion(Seccion seccion)
        {
            try
            {
                // Buscar la sección existente por ID
                var dbseccion = await _context.Seccions.FindAsync(seccion.IdSeccion);
                if (dbseccion == null)
                    return NotFound("Sección no encontrada");

                // Verificar que el maestro no tenga más de 4 clases
                var maestro = await _context.Maestros
                    .Include(m => m.Seccions)
                    .FirstOrDefaultAsync(m => m.IdMaestros == seccion.IdMaestro);

                // Verificar que el maestro no tenga clases a la misma hora
                var HorariosSeccion = await _context.Seccions
                    .Where(s => s.IdMaestro == seccion.IdMaestro && s.HoraInicial == seccion.HoraInicial)
                    .ToListAsync();

                if (HorariosSeccion.Any())
                {
                    return BadRequest("El maestro ya tiene una clase en el mismo horario.");
                }

                //Verificamos que el maestro no tenga más de 4 clases asignadas
                if (maestro != null && maestro.Seccions.Count >= 4)
                {
                    return BadRequest("El maestro ya tiene 4 clases asignadas.");
                }

                // Verificamos que la hora inicial esté entre las 8 am y las 4 pm
                if (seccion.HoraInicial < TimeSpan.FromHours(8) || seccion.HoraInicial > TimeSpan.FromHours(16))
                {
                    return BadRequest("La hora inicial debe estar entre las 8:00 a.m. y las 4:00 p.m.");
                }

                // Verificamos que la hora final esté entre las 9 am y las 5 pm
                if (seccion.HoraFinal < TimeSpan.FromHours(9) || seccion.HoraFinal > TimeSpan.FromHours(17))
                {
                    return BadRequest("La hora final debe estar entre las 9:00 a.m. y las 5:00 p.m.");
                }

                // Verificamos que la hora inicial y final no sean iguales
                if (seccion.HoraInicial == seccion.HoraFinal)
                {
                    return BadRequest("La hora inicial y final no pueden ser iguales.");
                }

                // Verificamos que la hora inicial no sea mayor que la hora final
                if (seccion.HoraInicial > seccion.HoraFinal)
                {
                    return BadRequest("La hora inicial no puede ser mayor que la hora final.");
                }

                // Verificamos que no haya otra sección en el mismo aula en la misma hora inicial
                var dbhoraseccion = await _context.Seccions
                    .FirstOrDefaultAsync(s => s.IdAula == seccion.IdAula && s.HoraInicial == seccion.HoraInicial);

                if (dbhoraseccion != null)
                {
                    return BadRequest("Ya existe una sección en el mismo aula en la misma hora inicial.");
                }

                // Si todas las validaciones pasan, actualiza la sección
                dbseccion.IdMaestro = seccion.IdMaestro;
                dbseccion.IdClase = seccion.IdClase;
                dbseccion.IdAula = seccion.IdAula;
                dbseccion.HoraInicial = seccion.HoraInicial;
                dbseccion.HoraFinal = seccion.HoraFinal;

                await _context.SaveChangesAsync();

                return Ok(await _context.Seccions.ToListAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Matricula>>> DeleteSeccion(int id)
        {
            try
            {
                var dbsecciondelete = await _context.Seccions.FindAsync(id);
                if (dbsecciondelete == null)
                    return BadRequest("Sección no encontrada");

                _context.Seccions.Remove(dbsecciondelete);
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