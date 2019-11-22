using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TasksSpa.Model;
using TasksSpa.Services.Contracts;
using TasksSpa.Services.Exceptions;

namespace TasksSpa.Services.Implementation
{
    public class AuthorizationApi : IAuthorizationApi
    {
        private readonly HttpClient httpClient;
        private const string server = "https://todos-rest-api-server.herokuapp.com";
        //private const string server = "http://localhost:5000";

        public AuthorizationApi(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<UserDto> GetUserInfo(string token)
        {
            return await httpClient.GetJsonAsync<UserDto>($"{server}/api/user/me?token={WebUtility.UrlEncode(token)}");
        }

        public async Task<string> Login(LoginParams loginParameters)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(loginParameters), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{server}/api/login", stringContent);
            var body = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) throw new NotAuthenticatedException(body);
            return body;
        }

        public async Task Logout()
        {
            await httpClient.PostAsync($"{server}/api/logout", null);
        }

        public async Task Register(RegistrationParams registerParameters)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(registerParameters), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{server}/api/registration", stringContent);
            var body = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) throw new RegistrationException(body);
        }
    }
}
