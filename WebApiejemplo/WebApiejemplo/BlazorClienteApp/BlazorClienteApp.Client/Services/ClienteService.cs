using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using BlazorClienteApp.Client.Models;

namespace BlazorClienteApp.Client.Services
{
    public class ClienteService : BaseService
    {
        public ClienteService(HttpClient httpClient) : base(httpClient) { }

        public async Task<List<Cliente>> GetClientesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Cliente>>("https://localhost:7051/api/clientes");
        }

        public async Task<Cliente> GetClienteByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Cliente>($"https://localhost:7051/api/clientes/{id}");
        }

        public async Task<Cliente> CreateClienteAsync(Cliente cliente)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7051/api/clientes", cliente);
            return await response.Content.ReadFromJsonAsync<Cliente>();
        }

        public async Task<Cliente> UpdateClienteAsync(int id, Cliente cliente)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7051/api/clientes/{id}", cliente);
            return await response.Content.ReadFromJsonAsync<Cliente>();
        }

        public async Task DeleteClienteAsync(int id)
        {
            await _httpClient.DeleteAsync($"https://localhost:7051/api/clientes/{id}");
        }
    }
}

