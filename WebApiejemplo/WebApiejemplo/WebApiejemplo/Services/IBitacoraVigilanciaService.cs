using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiejemplo.Models;

namespace WebApiejemplo.Services
{
    public interface IBitacoraVigilanciaService
    {
        Task<IEnumerable<BitacoraVigilancia>> GetAllAsync();
        Task<BitacoraVigilancia?> GetByIdAsync(int id);
        Task<BitacoraVigilancia> CreateAsync(BitacoraVigilancia entity);
        Task<BitacoraVigilancia?> UpdateAsync(int id, BitacoraVigilancia entity);
        Task<bool> DeleteAsync(int id);
    }
}
