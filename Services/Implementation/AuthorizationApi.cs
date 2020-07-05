using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TodosSpa.Model;
using TodosSpa.Services.Contracts;
using TodosSpa.Services.Exceptions;

namespace TodosSpa.Services.Implementation
{
    public class AuthorizationApi : IAuthorizationApi
    {
        private readonly HttpClient httpClient;

        public AuthorizationApi(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<UserDto> GetUserInfo(string token)
        {
            return await httpClient.GetJsonAsync<UserDto>($"{ServerInfo.Url}/api/user/me?token={WebUtility.UrlEncode(token)}");
        }

        public async Task<string> Login(LoginParams loginParameters)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(loginParameters), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{ServerInfo.Url}/api/login", stringContent);
            var body = await response.Content.ReadAsStringAsync();
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    break;
                case HttpStatusCode.Unauthorized:
                    throw new NotAuthenticatedException(body);
            }
            return body;
        }

        public async Task Logout(string token)
        {
            await httpClient.PostAsync($"{ServerInfo.Url}/api/logout?token={WebUtility.UrlEncode(token)}", new StringContent(""));
        }

        public async Task Register(RegistrationParams registerParameters)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(registerParameters), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{ServerInfo.Url}/api/registration", stringContent);
            var body = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) throw new RegistrationException(body);
        }
    }
}
