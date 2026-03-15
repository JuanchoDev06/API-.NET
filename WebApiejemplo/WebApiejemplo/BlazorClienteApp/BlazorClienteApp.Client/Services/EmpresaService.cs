using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using BlazorClienteApp.Client.Models;
using System.Text.Json;

namespace BlazorClienteApp.Client.Services
{
    public class EmpresaService : BaseService
    {
        public EmpresaService(HttpClient httpClient) : base(httpClient) { }

        public async Task<List<Empresa>> GetEmpresasAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Empresa>>("https://localhost:7051/api/empresas");
        }

        public async Task<Empresa> GetEmpresaByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Empresa>($"https://localhost:7051/api/empresas/{id}");
        }

        public async Task<Empresa> CreateEmpresaAsync(Empresa empresa)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("https://localhost:7051/api/empresas", empresa);
                response.EnsureSuccessStatusCode(); // Asegúrate de que la respuesta sea exitosa
                return await response.Content.ReadFromJsonAsync<Empresa>();
            }
            catch (HttpRequestException ex)
            {
                // Manejar errores de solicitud HTTP
                throw new Exception("Error al crear la empresa: " + ex.Message);
            }
            catch (NotSupportedException ex)
            {
                // Manejar errores de contenido no soportado
                throw new Exception("El formato de contenido no es soportado: " + ex.Message);
            }
            catch (JsonException ex)
            {
                // Manejar errores de JSON
                throw new Exception("Error al deserializar la respuesta JSON: " + ex.Message);
            }
        }

        public async Task<Empresa> UpdateEmpresaAsync(int id, Empresa empresa)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"https://localhost:7051/api/empresas/{id}", empresa);
                response.EnsureSuccessStatusCode(); // Asegúrate de que la respuesta sea exitosa
                return await response.Content.ReadFromJsonAsync<Empresa>();
            }
            catch (HttpRequestException ex)
            {
                // Manejar errores de solicitud HTTP
                throw new Exception("Error al actualizar la empresa: " + ex.Message);
            }
            catch (NotSupportedException ex)
            {
                // Manejar errores de contenido no soportado
                throw new Exception("El formato de contenido no es soportado: " + ex.Message);
            }
            catch (JsonException ex)
            {
                // Manejar errores de JSON
                throw new Exception("Error al deserializar la respuesta JSON: " + ex.Message);
            }
        }

        public async Task DeleteEmpresaAsync(int id)
        {
            await _httpClient.DeleteAsync($"https://localhost:7051/api/empresas/{id}");
        }
    }
}

