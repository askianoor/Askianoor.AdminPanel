﻿using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
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
            var savedToken = await _localStorageService.GetItemAsync<string>("Token");

            if (string.IsNullOrWhiteSpace(savedUser) || string.IsNullOrWhiteSpace(savedToken))
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
            _localStorageService.RemoveItemAsync("Token");
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
            status.MessageDescription = "Please enter your Username and Password Correctly!";

            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
                return status;

            var body = new { Username, Password };

            try
            {
                using (var client = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(body);
                    var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                    //HTTP GET
                    var responseTask = client.PostAsync(_appSettings.BaseAPIUri + "/AppUser/login", stringContent);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var responseString = result.Content.ReadAsStringAsync();
                        var response = JsonConvert.DeserializeObject<LoginResponse>(responseString.Result);

                        _localStorageService.SetItemAsync("Token", response.accessToken);
                        _localStorageService.SetItemAsync("Username", Username);

                        status.isSuccesful = true;
                        status.MessageTitle = "Authentication Succesful";
                        status.MessageDescription = "Welcome to the Admin Panel";
                        status.MessageType = "success";

                        return status;
                    }
                }
            }
            catch (Exception ex)
            {
                status.MessageTitle = "System Error";
                status.MessageDescription = ex.ToString();
                return status;
            }

            return status;
        }

        public Status ValidateRecaptcha(string gResponse)
        {
            Status status = new Status();
            status.isSuccesful = false;
            status.MessageType = "Danger";
            status.MessageTitle = "Authentication Error";
            status.MessageDescription = "The submission failed the spam bot verification. If you have JavaScript disabled in your browser, please enable it and try again.";

            using (var client = new System.Net.WebClient())
            {
                try
                {
                    string secretKey = _appSettings.SecretKey;
                    var gReply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, gResponse));

                    var jsonReturned = JsonConvert.DeserializeObject<ReCaptcha>(gReply);
                    if (jsonReturned.Success.ToLower() == "true")
                    {
                        status.isSuccesful = true;
                        status.MessageTitle = "Authentication Succesful";
                        status.MessageDescription = "Welcome to the Admin Panel";
                        status.MessageType = "success";
                    }
                    return status;
                }
                catch (Exception)
                {
                    return status;
                    throw;
                }
            }
        }
    }
}
