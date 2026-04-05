using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiejemplo.Data;
using WebApiejemplo.Models;

namespace WebApiejemplo.Services
{
    public class TorreService : ITorreService
    {
        private readonly ApplicationDbContext _context;

        public TorreService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Torre>> GetAllAsync()
        {
            return await _context.Torres.Include(t => t.Conjunto).ToListAsync();
        }

        public async Task<Torre?> GetByIdAsync(int id)
        {
            return await _context.Torres.Include(t => t.Conjunto).FirstOrDefaultAsync(t => t.TorreId == id);
        }

        public async Task<Torre> CreateAsync(Torre entity)
        {
            _context.Torres.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Torre?> UpdateAsync(int id, Torre entity)
        {
            var existing = await _context.Torres.FindAsync(id);
            if (existing == null) return null;

            existing.ConjuntoId = entity.ConjuntoId;
            existing.Nombre = entity.Nombre;

            _context.Torres.Update(existing);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Torres.FindAsync(id);
            if (entity == null) return false;

            _context.Torres.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
