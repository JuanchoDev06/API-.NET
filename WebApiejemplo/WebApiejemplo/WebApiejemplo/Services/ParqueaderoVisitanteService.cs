using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiejemplo.Data;
using WebApiejemplo.Models;

namespace WebApiejemplo.Services
{
    public class ParqueaderoVisitanteService : IParqueaderoVisitanteService
    {
        private readonly ApplicationDbContext _context;

        public ParqueaderoVisitanteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ParqueaderoVisitante>> GetAllAsync()
        {
            return await _context.ParqueaderosVisitantes.Include(p => p.Parqueadero).Include(p => p.Ingreso).ToListAsync();
        }

        public async Task<ParqueaderoVisitante?> GetByIdAsync(int id)
        {
            return await _context.ParqueaderosVisitantes.Include(p => p.Parqueadero).Include(p => p.Ingreso).FirstOrDefaultAsync(p => p.ParqueaderoVisitanteId == id);
        }

        public async Task<ParqueaderoVisitante> CreateAsync(ParqueaderoVisitante entity)
        {
            _context.ParqueaderosVisitantes.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<ParqueaderoVisitante?> UpdateAsync(int id, ParqueaderoVisitante entity)
        {
            var existing = await _context.ParqueaderosVisitantes.FindAsync(id);
            if (existing == null) return null;

            existing.ParqueaderoId = entity.ParqueaderoId;
            existing.Placa = entity.Placa;
            existing.FechaHoraIngreso = entity.FechaHoraIngreso;
            existing.FechaHoraSalida = entity.FechaHoraSalida;
            existing.IngresoId = entity.IngresoId;

            _context.ParqueaderosVisitantes.Update(existing);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.ParqueaderosVisitantes.FindAsync(id);
            if (entity == null) return false;

            _context.ParqueaderosVisitantes.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
