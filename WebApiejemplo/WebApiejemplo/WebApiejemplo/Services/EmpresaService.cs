using WebApiejemplo.Data;
using WebApiejemplo.Models;
using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Threading.Tasks;

namespace WebApiejemplo.Services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly ApplicationDbContext _context;

        public EmpresaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Empresa>> GetAllEmpresasAsync()
        {
            return await _context.EMPRESA.ToListAsync();
        }

        public async Task<Empresa> GetEmpresaByIdAsync(int id)
        {
            return await _context.EMPRESA.FindAsync(id);
        }

        public async Task<Empresa> CreateEmpresaAsync(Empresa empresa)
        {
            _context.EMPRESA.Add(empresa);
            await _context.SaveChangesAsync();
            return empresa;
        }

        public async Task<Empresa> UpdateEmpresaAsync(int id, Empresa empresa)
        {
            var existingEmpresa = await _context.EMPRESA.FindAsync(id);
            if (existingEmpresa == null) return null;

            existingEmpresa.NOMBREEMPRESA = empresa.NOMBREEMPRESA;
            await _context.SaveChangesAsync();
            return existingEmpresa;
        }

        public async Task<bool> DeleteEmpresaAsync(int id)
        {
            var empresa = await _context.EMPRESA.FindAsync(id);
            if (empresa == null) return false;

            _context.EMPRESA.Remove(empresa);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

