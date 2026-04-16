using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiejemplo.Data;
using WebApiejemplo.Models;

namespace WebApiejemplo.Services
{
    public class MantenimientoService : IMantenimientoService
    {
        private readonly ApplicationDbContext _context;

        public MantenimientoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Mantenimiento>> GetAllAsync()
        {
            return await _context.Mantenimientos.Include(m => m.TipoMantenimiento).Include(m => m.ZonaComun).ToListAsync();
        }

        public async Task<Mantenimiento?> GetByIdAsync(int id)
        {
            return await _context.Mantenimientos.Include(m => m.TipoMantenimiento).Include(m => m.ZonaComun).FirstOrDefaultAsync(m => m.MantenimientoId == id);
        }

        public async Task<Mantenimiento> CreateAsync(Mantenimiento entity)
        {
            _context.Mantenimientos.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Mantenimiento?> UpdateAsync(int id, Mantenimiento entity)
        {
            var existing = await _context.Mantenimientos.FindAsync(id);
            if (existing == null) return null;

            existing.TipoMantenimientoId = entity.TipoMantenimientoId;
            existing.Fecha = entity.Fecha;
            existing.Proveedor = entity.Proveedor;
            existing.Descripcion = entity.Descripcion;
            existing.Costo = entity.Costo;
            existing.ZonaComunId = entity.ZonaComunId;
            existing.Estado = entity.Estado;

            _context.Mantenimientos.Update(existing);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Mantenimientos.FindAsync(id);
            if (entity == null) return false;

            _context.Mantenimientos.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
