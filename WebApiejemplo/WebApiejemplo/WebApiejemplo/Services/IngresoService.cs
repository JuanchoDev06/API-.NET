using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiejemplo.Data;
using WebApiejemplo.Models;

namespace WebApiejemplo.Services
{
    public class IngresoService : IIngresoService
    {
        private readonly ApplicationDbContext _context;

        public IngresoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ingreso>> GetAllAsync()
        {
            return await _context.Ingresos.Include(i => i.Vigilante).Include(i => i.Unidad).ToListAsync();
        }

        public async Task<Ingreso?> GetByIdAsync(int id)
        {
            return await _context.Ingresos.Include(i => i.Vigilante).Include(i => i.Unidad).FirstOrDefaultAsync(i => i.IngresoId == id);
        }

        public async Task<Ingreso> CreateAsync(Ingreso entity)
        {
            _context.Ingresos.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Ingreso?> UpdateAsync(int id, Ingreso entity)
        {
            var existing = await _context.Ingresos.FindAsync(id);
            if (existing == null) return null;

            existing.Tipo = entity.Tipo;
            existing.NombrePersona = entity.NombrePersona;
            existing.Documento = entity.Documento;
            existing.FechaHoraIngreso = entity.FechaHoraIngreso;
            existing.FechaHoraSalida = entity.FechaHoraSalida;
            existing.UsuarioId = entity.UsuarioId;
            existing.UnidadId = entity.UnidadId;

            _context.Ingresos.Update(existing);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Ingresos.FindAsync(id);
            if (entity == null) return false;

            _context.Ingresos.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
