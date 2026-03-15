using System.Net.Http;

namespace BlazorClienteApp.Client.Services
{
    public abstract class BaseService
    {
        protected readonly HttpClient _httpClient;
        private string _token;

        public BaseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public void SetToken(string token)
        {
            _token = token;
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);
        }
    }
}

