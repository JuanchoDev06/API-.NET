using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiejemplo.Data;
using WebApiejemplo.Models;

namespace WebApiejemplo.Services
{
    public class BitacoraVigilanciaService : IBitacoraVigilanciaService
    {
        private readonly ApplicationDbContext _context;

        public BitacoraVigilanciaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BitacoraVigilancia>> GetAllAsync()
        {
            return await _context.BitacorasVigilancia.Include(b => b.Vigilante).ToListAsync();
        }

        public async Task<BitacoraVigilancia?> GetByIdAsync(int id)
        {
            return await _context.BitacorasVigilancia.Include(b => b.Vigilante).FirstOrDefaultAsync(b => b.BitacoraId == id);
        }

        public async Task<BitacoraVigilancia> CreateAsync(BitacoraVigilancia entity)
        {
            _context.BitacorasVigilancia.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<BitacoraVigilancia?> UpdateAsync(int id, BitacoraVigilancia entity)
        {
            var existing = await _context.BitacorasVigilancia.FindAsync(id);
            if (existing == null) return null;

            existing.VigilanteId = entity.VigilanteId;
            existing.FechaHora = entity.FechaHora;
            existing.Observacion = entity.Observacion;

            _context.BitacorasVigilancia.Update(existing);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.BitacorasVigilancia.FindAsync(id);
            if (entity == null) return false;

            _context.BitacorasVigilancia.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
