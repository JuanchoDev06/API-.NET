using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiejemplo.Data;
using WebApiejemplo.Models;

namespace WebApiejemplo.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly ApplicationDbContext _context;

        public UsuarioService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _context.Usuarios
                .Include(u => u.Rol)
                .Include(u => u.ResidentesUnidad)
                    .ThenInclude(ru => ru.Unidad)
                        .ThenInclude(a => a.Torre)
                .ToListAsync();
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            return await _context.Usuarios
                .Include(u => u.Rol)
                .Include(u => u.ResidentesUnidad)
                    .ThenInclude(ru => ru.Unidad)
                        .ThenInclude(a => a.Torre)
                .FirstOrDefaultAsync(u => u.UsuarioId == id);
        }

        public async Task<Usuario> CreateAsync(Usuario entity)
        {
            _context.Usuarios.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Usuario?> UpdateAsync(int id, Usuario entity)
        {
            var existing = await _context.Usuarios.FindAsync(id);
            if (existing == null) return null;
            existing.RolId = entity.RolId;
            existing.Nombre = entity.Nombre;
            existing.Documento = entity.Documento;
            existing.Email = entity.Email;
            existing.Telefono = entity.Telefono;
            existing.Activo = entity.Activo;
            existing.PasswordHash = entity.PasswordHash;
            _context.Usuarios.Update(existing);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Usuarios.FindAsync(id);
            if (entity == null) return false;
            _context.Usuarios.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}