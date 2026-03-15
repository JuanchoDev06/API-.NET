using WebApiejemplo.Models;
using WebApiejemplo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.Threading.Tasks;

namespace WebApiejemplo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresasController : ControllerBase
    {
        private readonly IEmpresaService _empresaService;

        public EmpresasController(IEmpresaService empresaService)
        {
            _empresaService = empresaService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Empresa>>> GetEmpresas()
        {
            var empresas = await _empresaService.GetAllEmpresasAsync();
            return Ok(empresas);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Empresa>> GetEmpresa(int id)
        {
            var empresa = await _empresaService.GetEmpresaByIdAsync(id);
            if (empresa == null)
                return NotFound();
            return Ok(empresa);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Empresa>> CreateEmpresa(Empresa empresa)
        {
            var createdEmpresa = await _empresaService.CreateEmpresaAsync(empresa);
            return CreatedAtAction(nameof(GetEmpresa), new { id = createdEmpresa.IDEMPRESA }, createdEmpresa);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Empresa>> UpdateEmpresa(int id, Empresa empresa)
        {
            var updatedEmpresa = await _empresaService.UpdateEmpresaAsync(id, empresa);
            if (updatedEmpresa == null)
                return NotFound();
            return Ok(updatedEmpresa);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteEmpresa(int id)
        {
            var success = await _empresaService.DeleteEmpresaAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}

