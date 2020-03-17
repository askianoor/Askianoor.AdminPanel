using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;



namespace Askianoor.AdminPanel.Data
{
    public class AskianoorAuthenticationStateProvider : AuthenticationStateProvider
    {

        private readonly ISessionStorageService _localStorageService;
        private readonly ApplicationSettings _appSettings;

        public AskianoorAuthenticationStateProvider(ISessionStorageService localStorageService, IOptions<ApplicationSettings> appSettings) 
        {
            _localStorageService = localStorageService;
            _appSettings = appSettings.Value;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var savedUser = await _localStorageService.GetItemAsync<string>("Username");

            if (string.IsNullOrWhiteSpace(savedUser))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var user = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, savedUser) }, "apiauth"));

            return await Task.FromResult(new AuthenticationState(user));
        }

        public void MarkUserAsAuthenticated(string Username)
        {
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, Username) }, "apiauth"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public void MarkUserAsLoggedOut()
        {
            _localStorageService.RemoveItemAsync("Username");
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(anonymousUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public Status ValidateUser(string Username, string Password)
        {
            Status status = new Status();
            status.isSuccesful = false;
            status.MessageType = "Danger";
            status.MessageTitle = "Authentication Error";
            status.MessageTitle = "Please your Username and Password Correctly";

            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
                return status;

            var body = new { Username, Password };

            using (var client = new HttpClient())
            {
                var json = JsonSerializer.Serialize(body);
                var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                //HTTP GET
                var responseTask = client.PostAsync(_appSettings.BaseAPIUri + "/AppUser/login", stringContent);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    status.isSuccesful = true;
                    status.MessageTitle = "Authentication Succesful";
                    status.MessageDescription = "Welcome to the Admin Panel";
                    status.MessageType = "success";

                    return status;
                }
            }
            return status;
        }

    }
}
