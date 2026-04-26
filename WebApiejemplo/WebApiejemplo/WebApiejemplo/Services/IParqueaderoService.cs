using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiejemplo.Models;

namespace WebApiejemplo.Services
{
    public interface IParqueaderoService
    {
        Task<IEnumerable<Parqueadero>> GetAllAsync();
        Task<Parqueadero?> GetByIdAsync(int id);
        Task<Parqueadero> CreateAsync(Parqueadero entity);
        Task<Parqueadero?> UpdateAsync(int id, Parqueadero entity);
        Task<bool> DeleteAsync(int id);
        Task<(bool success, string error)> AsignarAsync(int id, int unidadId, string? placa);
        Task<(bool success, string error)> DesasignarAsync(int id);
    }
}
