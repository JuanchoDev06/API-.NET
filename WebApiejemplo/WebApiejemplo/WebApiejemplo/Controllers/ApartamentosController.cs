using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiejemplo.Models;
using WebApiejemplo.Services;

namespace WebApiejemplo.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ApartamentosController : ControllerBase
    {
        private readonly IApartamentosService _service;

        public ApartamentosController(IApartamentosService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Apartamentos>>> GetAll()
        {
            var items = await _service.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Apartamentos>> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Apartamentos>> Create(Apartamentos entity)
        {
            var created = await _service.CreateAsync(entity);
            return StatusCode(201, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Apartamentos>> Update(int id, Apartamentos entity)
        {
            var updated = await _service.UpdateAsync(id, entity);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
