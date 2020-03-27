using Askianoor.AdminPanel.Data.Models;
using Blazored.SessionStorage;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Askianoor.AdminPanel.Data
{
    public class NavbarService
    {
        private readonly ISessionStorageService _localStorageService;
        private readonly ApplicationSettings _appSettings;

        public NavbarService(ISessionStorageService localStorageService, IOptions<ApplicationSettings> appSettings)
        {
            _localStorageService = localStorageService;
            _appSettings = appSettings.Value;
        }

        public async Task<List<Navbar>> GetNavbars()
        {
            string Token = await _localStorageService.GetItemAsync<string>("Token");

            if (string.IsNullOrEmpty(Token))
                return null;

            //var body = new { Token };

            using (var client = new HttpClient())
            {
                //var json = JsonConvert.SerializeObject(body);
                //var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                //HTTP GET
                var responseTask = client.GetAsync(_appSettings.BaseAPIUri + "/Navbars");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var responseString = result.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Navbar>>(responseString.Result);
                }
            }
            return null;
        }


        public async Task<Guid> AddNavbar(Navbar navbar)
        {
            string Token = await _localStorageService.GetItemAsync<string>("Token");

            if (string.IsNullOrEmpty(Token))
                return new Guid();

            //var body = new { navbar , Token };

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                var json = JsonConvert.SerializeObject(navbar);
                var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                //HTTP Post
                var responseTask = client.PostAsync(_appSettings.BaseAPIUri + "/Navbars", stringContent);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var responseString = result.Content.ReadAsStringAsync();
                    var resObject = JsonConvert.DeserializeObject<Navbar>(responseString.Result);
                    return resObject.MenuId;
                }
            }
            return new Guid();
        }

        public async Task<bool> UpdateNavbar(Navbar navbar)
        {
            string Token = await _localStorageService.GetItemAsync<string>("Token");

            if (string.IsNullOrEmpty(Token) || navbar.MenuId == Guid.Empty)
                return false;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                var json = JsonConvert.SerializeObject(navbar);
                var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                //HTTP Post
                var responseTask = client.PutAsync(_appSettings.BaseAPIUri + "/Navbars/" + navbar.MenuId, stringContent);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }


        public async Task<bool> RemoveNavbar(Navbar navbar)
        {
            string Token = await _localStorageService.GetItemAsync<string>("Token");

            if (string.IsNullOrEmpty(Token) || navbar.MenuId == Guid.Empty)
                return false;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                //HTTP Delete
                var responseTask = client.DeleteAsync(_appSettings.BaseAPIUri + "/Navbars/" + navbar.MenuId);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }
    }
}