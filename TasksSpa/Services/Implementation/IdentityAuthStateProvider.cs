using Blazor.Extensions.Storage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
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
            try
            {
                await localStorage.SetItem("token", await authorizationApi.Login(loginParameters));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task Register(RegistrationParams registerParameters)
        {
            try
            {
                await authorizationApi.Register(registerParameters);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task Logout()
        {
            try
            {
                await authorizationApi.Logout();
                userInfoCache = null;
                await localStorage.RemoveItem("token");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        private async Task<UserDto> GetUserInfo()
        {
            if (userInfoCache != null && userInfoCache.IsAuthenticated) return userInfoCache;
            try
            {
                userInfoCache = await
                authorizationApi
                .GetUserInfo(await localStorage.GetItem<string>("token"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
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
            catch (Exception ex)
            {
                Console.WriteLine("Request failed:" + ex.ToString());
            }

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        public async Task<string> GetToken() => await localStorage.GetItem<string>("token");
    }
}
