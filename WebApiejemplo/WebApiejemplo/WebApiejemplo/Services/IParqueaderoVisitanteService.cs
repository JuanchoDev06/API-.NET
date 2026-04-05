using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiejemplo.Models;

namespace WebApiejemplo.Services
{
    public interface IParqueaderoVisitanteService
    {
        Task<IEnumerable<ParqueaderoVisitante>> GetAllAsync();
        Task<ParqueaderoVisitante?> GetByIdAsync(int id);
        Task<ParqueaderoVisitante> CreateAsync(ParqueaderoVisitante entity);
        Task<ParqueaderoVisitante?> UpdateAsync(int id, ParqueaderoVisitante entity);
        Task<bool> DeleteAsync(int id);
    }
}
