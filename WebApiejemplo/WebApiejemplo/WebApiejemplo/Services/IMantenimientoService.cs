using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiejemplo.Models;

namespace WebApiejemplo.Services
{
    public interface IMantenimientoService
    {
        Task<IEnumerable<Mantenimiento>> GetAllAsync();
        Task<Mantenimiento?> GetByIdAsync(int id);
        Task<Mantenimiento> CreateAsync(Mantenimiento entity);
        Task<Mantenimiento?> UpdateAsync(int id, Mantenimiento entity);
        Task<bool> DeleteAsync(int id);
    }
}
