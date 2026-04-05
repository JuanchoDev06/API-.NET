using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiejemplo.Models;

namespace WebApiejemplo.Services
{
    public interface IIngresoService
    {
        Task<IEnumerable<Ingreso>> GetAllAsync();
        Task<Ingreso?> GetByIdAsync(int id);
        Task<Ingreso> CreateAsync(Ingreso entity);
        Task<Ingreso?> UpdateAsync(int id, Ingreso entity);
        Task<bool> DeleteAsync(int id);
    }
}
