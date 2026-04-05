using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiejemplo.Data;
using WebApiejemplo.Models;

namespace WebApiejemplo.Services
{
    public class MensajeriaService : IMensajeriaService
    {
        private readonly ApplicationDbContext _context;

        public MensajeriaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Mensajeria>> GetAllAsync()
        {
            return await _context.Mensajerias.Include(m => m.Unidad).Include(m => m.Vigilante).ToListAsync();
        }

        public async Task<Mensajeria?> GetByIdAsync(int id)
        {
            return await _context.Mensajerias.Include(m => m.Unidad).Include(m => m.Vigilante).FirstOrDefaultAsync(m => m.MensajeriaId == id);
        }

        public async Task<Mensajeria> CreateAsync(Mensajeria entity)
        {
            _context.Mensajerias.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Mensajeria?> UpdateAsync(int id, Mensajeria entity)
        {
            var existing = await _context.Mensajerias.FindAsync(id);
            if (existing == null) return null;

            existing.Empresa = entity.Empresa;
            existing.Guia = entity.Guia;
            existing.FechaRecepcion = entity.FechaRecepcion;
            existing.FechaEntrega = entity.FechaEntrega;
            existing.UnidadId = entity.UnidadId;
            existing.UsuarioId = entity.UsuarioId;

            _context.Mensajerias.Update(existing);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Mensajerias.FindAsync(id);
            if (entity == null) return false;

            _context.Mensajerias.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
