using WebApiejemplo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApiejemplo.Services
{
    public interface IEmpresaService
    {
        Task<List<Empresa>> GetAllEmpresasAsync();
        Task<Empresa> GetEmpresaByIdAsync(int id);
        Task<Empresa> CreateEmpresaAsync(Empresa empresa);
        Task<Empresa> UpdateEmpresaAsync(int id, Empresa empresa);
        Task<bool> DeleteEmpresaAsync(int id);
    }
}

