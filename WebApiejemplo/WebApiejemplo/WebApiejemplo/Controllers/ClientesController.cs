using WebApiejemplo.Models;
using WebApiejemplo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApiejemplo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            var clientes = await _clienteService.GetAllClientesAsync();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _clienteService.GetClienteByIdAsync(id);
            if (cliente == null)
                return NotFound();
            return Ok(cliente);
        }

        [HttpPost]
        //[Authorize]
        public async Task<ActionResult<Cliente>> CreateCliente(Cliente cliente)
        {
            var createdCliente = await _clienteService.CreateClienteAsync(cliente);
            return CreatedAtAction(nameof(GetCliente), new { id = createdCliente.ID }, createdCliente);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Cliente>> UpdateCliente(int id, Cliente cliente)
        {
            var updatedCliente = await _clienteService.UpdateClienteAsync(id, cliente);
            if (updatedCliente == null)
                return NotFound();
            return Ok(updatedCliente);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var success = await _clienteService.DeleteClienteAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}
