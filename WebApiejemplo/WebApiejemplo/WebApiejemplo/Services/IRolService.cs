using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiejemplo.Models;

namespace WebApiejemplo.Services
{
    public interface IRolService
    {
        Task<IEnumerable<Rol>> GetAllAsync();
        Task<Rol?> GetByIdAsync(int id);
        Task<Rol> CreateAsync(Rol entity);
        Task<Rol?> UpdateAsync(int id, Rol entity);
        Task<bool> DeleteAsync(int id);
    }
}
