using WebApiejemplo.Models;

namespace WebApiejemplo.Services
{
    public interface IClienteService
    {
        Task<List<Cliente>> GetAllClientesAsync();
        Task<Cliente> GetClienteByIdAsync(int id);
        Task<Cliente>GetClienteByEmailAndPwd(string email, string password);
        Task<Cliente> CreateClienteAsync(Cliente cliente);
        Task<Cliente> UpdateClienteAsync(int id, Cliente cliente);
        Task<bool> DeleteClienteAsync(int id);
    }
}
