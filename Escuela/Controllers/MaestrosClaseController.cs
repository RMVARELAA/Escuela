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
            return Ok(await _context.MaestrosClases.ToListAsync());
        }

        //[HttpPost]
        //public async Task<IActionResult> 
    }
}
