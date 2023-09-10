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
            ////Verificamos que no se pueda agregar una Clase con el mismo nombre.
            //var nombreclase = _context.Clases.FirstOrDefault(a => a.Clase1 == clase.Clase1);
            //if (nombreclase != null)
            //{
            //    return BadRequest("¡Ya existe una clase con el mismo nombre!");
            //}

            ////Verificamos que no se pueda agregar una Clase en la misma aula.
            //var aulaclase = _context.Clases.FirstOrDefault(a => a.AulaId == clase.AulaId);
            //if (aulaclase != null)
            //{
            //    return BadRequest("¡Ya existe una clase en esta aula!");
            //}

            //Si no hay datos duplicados se procede a guardar los datos en la base de datos.
            _context.MatriculaAlumnos.Add(matriculaAlumno);
            await _context.SaveChangesAsync();

            return Ok(await _context.MatriculaAlumnos.ToListAsync());
        }

    }
}
