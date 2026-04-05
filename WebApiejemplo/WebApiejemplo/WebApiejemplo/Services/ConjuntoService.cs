using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiejemplo.Data;
using WebApiejemplo.Models;

namespace WebApiejemplo.Services
{
    public class ConjuntoService : IConjuntoService
    {
        private readonly ApplicationDbContext _context;

        public ConjuntoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Conjunto>> GetAllAsync()
        {
            return await _context.Conjuntos.ToListAsync();
        }

        public async Task<Conjunto?> GetByIdAsync(int id)
        {
            return await _context.Conjuntos.FindAsync(id);
        }

        public async Task<Conjunto> CreateAsync(Conjunto entity)
        {
            _context.Conjuntos.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Conjunto?> UpdateAsync(int id, Conjunto entity)
        {
            var existing = await _context.Conjuntos.FindAsync(id);
            if (existing == null) return null;

            existing.Nombre = entity.Nombre;
            existing.Direccion = entity.Direccion;
            existing.Ciudad = entity.Ciudad;
            existing.NIT = entity.NIT;
            existing.Telefono = entity.Telefono;

            _context.Conjuntos.Update(existing);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Conjuntos.FindAsync(id);
            if (entity == null) return false;

            _context.Conjuntos.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
