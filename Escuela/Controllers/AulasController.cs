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
        public async Task<IActionResult> GetAulas()
        {
            return Ok(await _context.Aulas.ToListAsync());
        }

        //[HttpPost]
        //public async Task<IActionResult> PostAulas()
        //{
        //    return Ok(await _context.Aulas.)
        //}
    }
}
