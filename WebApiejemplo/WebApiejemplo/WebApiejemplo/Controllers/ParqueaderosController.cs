using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WebApiejemplo.Models;
using WebApiejemplo.Services;

namespace WebApiejemplo.Controllers
{
    [Authorize(Roles = "Administrador,Vigilante")]
    [Route("api/[controller]")]
    [ApiController]
    public class ParqueaderosController : ControllerBase
    {
        private readonly IParqueaderoService _service;

        public ParqueaderosController(IParqueaderoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Parqueadero>>> GetAll()
        {
            var items = await _service.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Parqueadero>> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Parqueadero>> Create(Parqueadero entity)
        {
            var created = await _service.CreateAsync(entity);
            return StatusCode(201, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Parqueadero>> Update(int id, Parqueadero entity)
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

        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}/asignar")]
        public async Task<ActionResult> Asignar(int id, [FromBody] AsignarParqueaderoRequest request)
        {
            var (success, error) = await _service.AsignarAsync(id, request.UnidadId, request.Placa);
            if (!success) return BadRequest(new { mensaje = error });
            return Ok(new { mensaje = "Cupo asignado correctamente." });
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}/desasignar")]
        public async Task<ActionResult> Desasignar(int id)
        {
            var (success, error) = await _service.DesasignarAsync(id);
            if (!success) return BadRequest(new { mensaje = error });
            return Ok(new { mensaje = "Cupo liberado correctamente." });
        }
    }

    public class AsignarParqueaderoRequest
    {
        [Required]
        public int UnidadId { get; set; }

        [MaxLength(20)]
        public string? Placa { get; set; }
    }
}
