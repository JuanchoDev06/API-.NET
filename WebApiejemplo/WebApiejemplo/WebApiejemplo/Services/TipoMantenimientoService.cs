using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiejemplo.Data;
using WebApiejemplo.Models;

namespace WebApiejemplo.Services
{
    public class TipoMantenimientoService : ITipoMantenimientoService
    {
        private readonly ApplicationDbContext _context;

        public TipoMantenimientoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TipoMantenimiento>> GetAllAsync()
        {
            return await _context.TiposMantenimiento.ToListAsync();
        }

        public async Task<TipoMantenimiento?> GetByIdAsync(int id)
        {
            return await _context.TiposMantenimiento.FindAsync(id);
        }

        public async Task<TipoMantenimiento> CreateAsync(TipoMantenimiento entity)
        {
            _context.TiposMantenimiento.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TipoMantenimiento?> UpdateAsync(int id, TipoMantenimiento entity)
        {
            var existing = await _context.TiposMantenimiento.FindAsync(id);
            if (existing == null) return null;

            existing.Nombre = entity.Nombre;

            _context.TiposMantenimiento.Update(existing);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.TiposMantenimiento.FindAsync(id);
            if (entity == null) return false;

            _context.TiposMantenimiento.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
