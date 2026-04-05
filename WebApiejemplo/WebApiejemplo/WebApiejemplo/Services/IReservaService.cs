using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiejemplo.Models;

namespace WebApiejemplo.Services
{
    public interface IReservaService
    {
        Task<IEnumerable<Reserva>> GetAllAsync();
        Task<Reserva?> GetByIdAsync(int id);
        Task<Reserva> CreateAsync(Reserva entity);
        Task<Reserva?> UpdateAsync(int id, Reserva entity);
        Task<bool> DeleteAsync(int id);
    }
}
