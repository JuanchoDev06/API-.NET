using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiejemplo.Models;

namespace WebApiejemplo.Services
{
    public interface IMensajeriaService
    {
        Task<IEnumerable<Mensajeria>> GetAllAsync();
        Task<Mensajeria?> GetByIdAsync(int id);
        Task<Mensajeria> CreateAsync(Mensajeria entity);
        Task<Mensajeria?> UpdateAsync(int id, Mensajeria entity);
        Task<bool> DeleteAsync(int id);
    }
}
