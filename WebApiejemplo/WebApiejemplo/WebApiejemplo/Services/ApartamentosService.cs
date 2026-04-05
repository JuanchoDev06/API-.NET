using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiejemplo.Data;
using WebApiejemplo.Models;

namespace WebApiejemplo.Services
{
    public class ApartamentosService : IApartamentosService
    {
        private readonly ApplicationDbContext _context;

        public ApartamentosService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Apartamentos>> GetAllAsync()
        {
            return await _context.Apartamentos.Include(a => a.Torre).ToListAsync();
        }

        public async Task<Apartamentos?> GetByIdAsync(int id)
        {
            return await _context.Apartamentos.Include(a => a.Torre).FirstOrDefaultAsync(a => a.UnidadId == id);
        }

        public async Task<Apartamentos> CreateAsync(Apartamentos entity)
        {
            _context.Apartamentos.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Apartamentos?> UpdateAsync(int id, Apartamentos entity)
        {
            var existing = await _context.Apartamentos.FindAsync(id);
            if (existing == null) return null;

            existing.TorreId = entity.TorreId;
            existing.Numero = entity.Numero;
            existing.Tipo = entity.Tipo;
            existing.Area = entity.Area;

            _context.Apartamentos.Update(existing);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Apartamentos.FindAsync(id);
            if (entity == null) return false;

            _context.Apartamentos.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
