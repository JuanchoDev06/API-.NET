using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiejemplo.Data;
using WebApiejemplo.Models;

namespace WebApiejemplo.Services
{
    public class ParqueaderoService : IParqueaderoService
    {
        private readonly ApplicationDbContext _context;

        public ParqueaderoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Parqueadero>> GetAllAsync()
        {
            return await _context.Parqueaderos.Include(p => p.Unidad).ToListAsync();
        }

        public async Task<Parqueadero?> GetByIdAsync(int id)
        {
            return await _context.Parqueaderos.Include(p => p.Unidad).FirstOrDefaultAsync(p => p.ParqueaderoId == id);
        }

        public async Task<Parqueadero> CreateAsync(Parqueadero entity)
        {
            _context.Parqueaderos.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Parqueadero?> UpdateAsync(int id, Parqueadero entity)
        {
            var existing = await _context.Parqueaderos.FindAsync(id);
            if (existing == null) return null;

            existing.Tipo = entity.Tipo;
            existing.Numero = entity.Numero;
            existing.UnidadId = entity.UnidadId;

            _context.Parqueaderos.Update(existing);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Parqueaderos.FindAsync(id);
            if (entity == null) return false;

            _context.Parqueaderos.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
