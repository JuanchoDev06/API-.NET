using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorClienteApp.Client.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private string _token;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> LoginAsync(string username, string password)
        {
            var loginRequest = new { Username = username, Password = password };
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7051/api/auth/login", loginRequest);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                _token = result.Token;
                return _token;
            }

            return null;
        }

        public bool IsTokenValid()
        {
            return !string.IsNullOrEmpty(_token);
        }

        private class LoginResponse
        {
            public string Token { get; set; }
        }
    }
}

