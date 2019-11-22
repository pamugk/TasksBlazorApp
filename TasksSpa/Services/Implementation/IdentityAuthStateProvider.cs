using Blazor.Extensions.Storage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using TasksSpa.Model;
using TasksSpa.Services.Contracts;

namespace TasksSpa.Services.Implementation
{
    public class IdentityAuthStateProvider : AuthenticationStateProvider
    {
        private readonly IAuthorizationApi authorizationApi;
        private readonly LocalStorage localStorage;
        private UserDto userInfoCache;

        public IdentityAuthStateProvider(IAuthorizationApi authorizationApi, LocalStorage localStorage)
        {
            this.authorizationApi = authorizationApi;
            this.localStorage = localStorage;
        }

        public async Task Login(LoginParams loginParameters)
        {
            await localStorage.SetItem("token", await authorizationApi.Login(loginParameters));
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task Register(RegistrationParams registerParameters)
        {
            await authorizationApi.Register(registerParameters);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task Logout()
        {
            await authorizationApi.Logout();
            userInfoCache = null;
            await localStorage.RemoveItem("token");
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        private async Task<UserDto> GetUserInfo()
        {
            if (userInfoCache != null && userInfoCache.IsAuthenticated) return userInfoCache;
            userInfoCache = await 
                authorizationApi
                .GetUserInfo(await localStorage.GetItem<string>("token"));
            return userInfoCache;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            try
            {
                userInfoCache = await GetUserInfo();
                if (userInfoCache.IsAuthenticated)
                    identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, userInfoCache.Login) },
                        "Token server authentication");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Request failed:" + ex.ToString());
            }

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        public async Task<string> GetToken() => await localStorage.GetItem<string>("token");
    }
}
