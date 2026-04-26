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
            existing.Placa = entity.Placa;

            _context.Parqueaderos.Update(existing);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<(bool success, string error)> AsignarAsync(int id, int unidadId, string? placa)
        {
            var parqueadero = await _context.Parqueaderos.FindAsync(id);
            if (parqueadero == null)
                return (false, "Parqueadero no encontrado.");

            if (parqueadero.UnidadId.HasValue)
                return (false, "El cupo ya está asignado a otro apartamento.");

            parqueadero.UnidadId = unidadId;
            parqueadero.Placa = placa;
            await _context.SaveChangesAsync();
            return (true, string.Empty);
        }

        public async Task<(bool success, string error)> DesasignarAsync(int id)
        {
            var parqueadero = await _context.Parqueaderos.FindAsync(id);
            if (parqueadero == null)
                return (false, "Parqueadero no encontrado.");

            if (!parqueadero.UnidadId.HasValue)
                return (false, "El cupo no está asignado a ningún apartamento.");

            parqueadero.UnidadId = null;
            parqueadero.Placa = null;
            await _context.SaveChangesAsync();
            return (true, string.Empty);
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
