using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiejemplo.Data;
using WebApiejemplo.Models;

namespace WebApiejemplo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuartosUtilController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CuartosUtilController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetCuartos()
        {
            return await _context.CuartosUtil
                .Include(c => c.Unidad)
                    .ThenInclude(u => u.Torre)
                .Select(c => new {
                    c.CuartoUtilId,
                    c.Numero,
                    c.Descripcion,
                    c.UnidadId,
                    Unidad = c.Unidad != null ? new
                    {
                        c.Unidad.UnidadId,
                        c.Unidad.Numero,
                        Torre = c.Unidad.Torre != null ? new { c.Unidad.Torre.Nombre } : null
                    } : null
                })
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<CuartoUtil>> PostCuarto(CuartoUtil cuarto)
        {
            _context.CuartosUtil.Add(cuarto);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCuarto(int id, CuartoUtil cuarto)
        {
            if (id != cuarto.CuartoUtilId) return BadRequest();
            _context.Entry(cuarto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCuarto(int id)
        {
            var cuarto = await _context.CuartosUtil.FindAsync(id);
            if (cuarto == null) return NotFound();
            _context.CuartosUtil.Remove(cuarto);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
