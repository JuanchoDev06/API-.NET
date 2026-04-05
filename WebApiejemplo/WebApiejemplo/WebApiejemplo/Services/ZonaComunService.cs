using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiejemplo.Data;
using WebApiejemplo.Models;

namespace WebApiejemplo.Services
{
    public class ZonaComunService : IZonaComunService
    {
        private readonly ApplicationDbContext _context;

        public ZonaComunService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ZonaComun>> GetAllAsync()
        {
            return await _context.ZonasComunes.ToListAsync();
        }

        public async Task<ZonaComun?> GetByIdAsync(int id)
        {
            return await _context.ZonasComunes.FindAsync(id);
        }

        public async Task<ZonaComun> CreateAsync(ZonaComun entity)
        {
            _context.ZonasComunes.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<ZonaComun?> UpdateAsync(int id, ZonaComun entity)
        {
            var existing = await _context.ZonasComunes.FindAsync(id);
            if (existing == null) return null;

            existing.Nombre = entity.Nombre;
            existing.RequierePago = entity.RequierePago;
            existing.ValorHora = entity.ValorHora;

            _context.ZonasComunes.Update(existing);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.ZonasComunes.FindAsync(id);
            if (entity == null) return false;

            _context.ZonasComunes.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
