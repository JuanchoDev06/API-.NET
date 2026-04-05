using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiejemplo.Models;

namespace WebApiejemplo.Services
{
    public interface ITorreService
    {
        Task<IEnumerable<Torre>> GetAllAsync();
        Task<Torre?> GetByIdAsync(int id);
        Task<Torre> CreateAsync(Torre entity);
        Task<Torre?> UpdateAsync(int id, Torre entity);
        Task<bool> DeleteAsync(int id);
    }
}
