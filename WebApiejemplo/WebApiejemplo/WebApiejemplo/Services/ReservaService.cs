using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiejemplo.Data;
using WebApiejemplo.Models;

namespace WebApiejemplo.Services
{
    public class ReservaService : IReservaService
    {
        private readonly ApplicationDbContext _context;

        public ReservaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reserva>> GetAllAsync()
        {
            return await _context.Reservas.Include(r => r.ZonaComun).Include(r => r.Usuario).ToListAsync();
        }

        public async Task<Reserva?> GetByIdAsync(int id)
        {
            return await _context.Reservas.Include(r => r.ZonaComun).Include(r => r.Usuario).FirstOrDefaultAsync(r => r.ReservaId == id);
        }

        public async Task<Reserva> CreateAsync(Reserva entity)
        {
            _context.Reservas.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Reserva?> UpdateAsync(int id, Reserva entity)
        {
            var existing = await _context.Reservas.FindAsync(id);
            if (existing == null) return null;

            existing.ZonaComunId = entity.ZonaComunId;
            existing.UsuarioId = entity.UsuarioId;
            existing.Fecha = entity.Fecha;
            existing.HoraInicio = entity.HoraInicio;
            existing.HoraFin = entity.HoraFin;
            existing.Estado = entity.Estado;

            _context.Reservas.Update(existing);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Reservas.FindAsync(id);
            if (entity == null) return false;

            _context.Reservas.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
