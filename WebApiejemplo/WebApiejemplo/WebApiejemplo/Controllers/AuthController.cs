using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using WebApiejemplo.Helpers;
using WebApiejemplo.Models;
using WebApiejemplo.Services;

namespace WebApiejemplo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration, IClienteService clienteService)
        {
            _configuration = configuration;
            _clienteService = clienteService;
        }

        // Endpoint para la autenticación
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest loginRequest)
        {
            // Usar ClienteService para buscar el cliente por username (email) y contraseña
            var cliente = await _clienteService.GetClienteByEmailAndPwd(loginRequest.Username, loginRequest.Password);

            if (cliente == null)
            {
                return Unauthorized(new { message = "Username or password is incorrect" });
            }

            // Opcionalmente, podriamos usar el Email del cliente para generar el JWT
            // var token = JwtHelper.GenerateJwtToken(cliente.Email ?? loginRequest.Username, _configuration);
            var token = JwtHelper.GenerateJwtToken(loginRequest.Username, _configuration);

            return Ok(new { Token = token });
        }
    }
}
