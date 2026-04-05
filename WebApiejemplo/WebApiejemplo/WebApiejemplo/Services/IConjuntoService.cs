using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiejemplo.Models;

namespace WebApiejemplo.Services
{
    public interface IConjuntoService
    {
        Task<IEnumerable<Conjunto>> GetAllAsync();
        Task<Conjunto?> GetByIdAsync(int id);
        Task<Conjunto> CreateAsync(Conjunto entity);
        Task<Conjunto?> UpdateAsync(int id, Conjunto entity);
        Task<bool> DeleteAsync(int id);
    }
}
