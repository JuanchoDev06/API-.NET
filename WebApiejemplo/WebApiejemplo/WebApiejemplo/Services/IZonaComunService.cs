using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiejemplo.Models;

namespace WebApiejemplo.Services
{
    public interface IZonaComunService
    {
        Task<IEnumerable<ZonaComun>> GetAllAsync();
        Task<ZonaComun?> GetByIdAsync(int id);
        Task<ZonaComun> CreateAsync(ZonaComun entity);
        Task<ZonaComun?> UpdateAsync(int id, ZonaComun entity);
        Task<bool> DeleteAsync(int id);
    }
}
