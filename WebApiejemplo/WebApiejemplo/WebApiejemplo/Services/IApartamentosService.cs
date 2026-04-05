using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiejemplo.Models;

namespace WebApiejemplo.Services
{
    public interface IApartamentosService
    {
        Task<IEnumerable<Apartamentos>> GetAllAsync();
        Task<Apartamentos?> GetByIdAsync(int id);
        Task<Apartamentos> CreateAsync(Apartamentos entity);
        Task<Apartamentos?> UpdateAsync(int id, Apartamentos entity);
        Task<bool> DeleteAsync(int id);
    }
}
