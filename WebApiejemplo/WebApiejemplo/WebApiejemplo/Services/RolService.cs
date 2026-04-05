using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiejemplo.Data;
using WebApiejemplo.Models;

namespace WebApiejemplo.Services
{
    public class RolService : IRolService
    {
        private readonly ApplicationDbContext _context;

        public RolService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rol>> GetAllAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Rol?> GetByIdAsync(int id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public async Task<Rol> CreateAsync(Rol entity)
        {
            _context.Roles.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Rol?> UpdateAsync(int id, Rol entity)
        {
            var existing = await _context.Roles.FindAsync(id);
            if (existing == null) return null;

            existing.Nombre = entity.Nombre;

            _context.Roles.Update(existing);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Roles.FindAsync(id);
            if (entity == null) return false;

            _context.Roles.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
