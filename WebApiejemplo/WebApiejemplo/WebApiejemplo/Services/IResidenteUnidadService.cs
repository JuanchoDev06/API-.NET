using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiejemplo.Models;

namespace WebApiejemplo.Services
{
    public interface IResidenteUnidadService
    {
        Task<IEnumerable<ResidenteUnidad>> GetAllAsync();
        Task<ResidenteUnidad?> GetByIdAsync(int id);
        Task<ResidenteUnidad> CreateAsync(ResidenteUnidad entity);
        Task<ResidenteUnidad?> UpdateAsync(int id, ResidenteUnidad entity);
        Task<bool> DeleteAsync(int id);
    }
}
