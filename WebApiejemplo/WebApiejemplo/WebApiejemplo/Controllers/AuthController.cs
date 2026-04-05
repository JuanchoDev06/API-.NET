using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiejemplo.Models;
using WebApiejemplo.Models.DTOs;
using WebApiejemplo.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;

namespace WebApiejemplo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Rol)
                .SingleOrDefaultAsync(u => u.Documento == request.Documento);

            if (usuario == null)
                return Unauthorized("Credenciales inválidas.");

            if (!BCrypt.Net.BCrypt.Verify(request.Password, usuario.PasswordHash))
                return Unauthorized("Credenciales inválidas.");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioId.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(ClaimTypes.Email, usuario.Email ?? string.Empty),
                new Claim(ClaimTypes.Role, usuario.Rol?.Nombre ?? string.Empty)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiryMinutes"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            var generatedToken = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new
            {
                token = generatedToken,
                expiration = expiration,
                usuarioId = usuario.UsuarioId,
                nombre = usuario.Nombre,
                rol = usuario.Rol?.Nombre
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (await _context.Usuarios.AnyAsync(u => u.Documento == request.Documento))
            {
                return BadRequest("El documento ya se encuentra registrado.");
            }

            var nuevoUsuario = new Usuario
            {
                Nombre = request.Nombre,
                Documento = request.Documento,
                Email = request.Email,
                Telefono = request.Telefono,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                RolId = request.RolId,
                Activo = true,
                FechaCreacion = DateTime.Now
            };

            _context.Usuarios.Add(nuevoUsuario);
            await _context.SaveChangesAsync();

            return StatusCode(201, new
            {
                nuevoUsuario.UsuarioId,
                nuevoUsuario.Nombre,
                nuevoUsuario.Documento,
                nuevoUsuario.Email,
                nuevoUsuario.Telefono,
                nuevoUsuario.RolId
            });
        }
    }
}
