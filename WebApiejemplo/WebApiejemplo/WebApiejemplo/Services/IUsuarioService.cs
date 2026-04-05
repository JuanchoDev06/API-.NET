using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiejemplo.Models;

namespace WebApiejemplo.Services
{
    public interface IUsuarioService
    {
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario?> GetByIdAsync(int id);
        Task<Usuario> CreateAsync(Usuario entity);
        Task<Usuario?> UpdateAsync(int id, Usuario entity);
        Task<bool> DeleteAsync(int id);
    }
}
