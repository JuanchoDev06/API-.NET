using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiejemplo.Data;
using WebApiejemplo.Models;

namespace WebApiejemplo.Services
{
    public class ResidenteUnidadService : IResidenteUnidadService
    {
        private readonly ApplicationDbContext _context;

        public ResidenteUnidadService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ResidenteUnidad>> GetAllAsync()
        {
            return await _context.ResidentesUnidad.Include(r => r.Usuario).Include(r => r.Unidad).ToListAsync();
        }

        public async Task<ResidenteUnidad?> GetByIdAsync(int id)
        {
            return await _context.ResidentesUnidad.Include(r => r.Usuario).Include(r => r.Unidad).FirstOrDefaultAsync(r => r.ResidenteUnidadId == id);
        }

        public async Task<ResidenteUnidad> CreateAsync(ResidenteUnidad entity)
        {
            _context.ResidentesUnidad.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<ResidenteUnidad?> UpdateAsync(int id, ResidenteUnidad entity)
        {
            var existing = await _context.ResidentesUnidad.FindAsync(id);
            if (existing == null) return null;

            existing.UsuarioId = entity.UsuarioId;
            existing.UnidadId = entity.UnidadId;
            existing.EsPropietario = entity.EsPropietario;

            _context.ResidentesUnidad.Update(existing);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.ResidentesUnidad.FindAsync(id);
            if (entity == null) return false;

            _context.ResidentesUnidad.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
