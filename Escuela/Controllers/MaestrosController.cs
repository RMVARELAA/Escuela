using Escuela.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Escuela.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaestrosController : ControllerBase
    {
        private readonly EscuelaContext _context;

        public MaestrosController(EscuelaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetMaestros()
        {
            return Ok(await _context.Maestros.ToListAsync());
        }

        [HttpPost]
        public async Task<Maestro> PostMaestros(Maestro maestros)
        {
            _context.Maestros.Add(Maestro);
            _context.SaveChanges();
            return Ok(await _context.Maestros.ToListAsync());

        }
    }
}
