using WebApiejemplo.Data;
using WebApiejemplo.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApiejemplo.Services
{
    public class ClienteService : IClienteService
    {
        private readonly ApplicationDbContext _context;

        public ClienteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Cliente>> GetAllClientesAsync()
        {
            return await _context.CLIENTE.ToListAsync();
        }

        public async Task<Cliente> GetClienteByIdAsync(int id)
        {
            return await _context.CLIENTE.FindAsync(id);
        }
        public async Task<Cliente> GetClienteByEmailAndPwd(string email, string password)
        {
            return await _context.CLIENTE.FirstOrDefaultAsync(c => c.Email == email && c.Password == password);
        }

        public async Task<Cliente> CreateClienteAsync(Cliente cliente)
        {
            try
            {
                _context.CLIENTE.Add(cliente);
                await _context.SaveChangesAsync();
                return cliente;
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }

        public async Task<Cliente> UpdateClienteAsync(int id, Cliente cliente)
        {
            var existingCliente = await _context.CLIENTE.FindAsync(id);
            if (existingCliente == null) return null;

            existingCliente.NIT = cliente.NIT;
            existingCliente.NOMBRE = cliente.NOMBRE;
            await _context.SaveChangesAsync();
            return existingCliente;
        }

        public async Task<bool> DeleteClienteAsync(int id)
        {
            var cliente = await _context.CLIENTE.FindAsync(id);
            if (cliente == null) return false;

            _context.CLIENTE.Remove(cliente);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
