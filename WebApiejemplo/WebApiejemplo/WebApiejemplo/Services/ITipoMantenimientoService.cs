using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiejemplo.Models;

namespace WebApiejemplo.Services
{
    public interface ITipoMantenimientoService
    {
        Task<IEnumerable<TipoMantenimiento>> GetAllAsync();
        Task<TipoMantenimiento?> GetByIdAsync(int id);
        Task<TipoMantenimiento> CreateAsync(TipoMantenimiento entity);
        Task<TipoMantenimiento?> UpdateAsync(int id, TipoMantenimiento entity);
        Task<bool> DeleteAsync(int id);
    }
}
