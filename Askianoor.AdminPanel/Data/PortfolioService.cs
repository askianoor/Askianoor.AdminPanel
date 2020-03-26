using Askianoor.AdminPanel.Data.Models;
using Blazored.SessionStorage;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Askianoor.AdminPanel.Data
{
    public class PortfolioService
    {
        private readonly HttpClient _httpClient;
        private readonly ApplicationSettings _appSettings;
        private readonly ISessionStorageService _localStorageService;

        public PortfolioService(ISessionStorageService localStorageService, IOptions<ApplicationSettings> appSettings)
        {
            _localStorageService = localStorageService;
            _appSettings = appSettings.Value;
            _httpClient = new HttpClient();
        }

        public List<Portfolio> GetPortfolios()
        {
            //HTTP GET
            var responseTask = _httpClient.GetAsync(_appSettings.BaseAPIUri + "/Portfolios");
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var responseString = result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Portfolio>>(responseString.Result);
            }

            return null;
        }


        public async Task<Guid> AddPortfolio(Portfolio portfolio)
        {
            string Token = await _localStorageService.GetItemAsync<string>("Token");

            if (string.IsNullOrEmpty(Token)) return new Guid();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            var json = JsonConvert.SerializeObject(portfolio);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            //HTTP Post
            var responseTask = _httpClient.PostAsync(_appSettings.BaseAPIUri + "/Portfolios", stringContent);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var responseString = result.Content.ReadAsStringAsync();
                var resObject = JsonConvert.DeserializeObject<Portfolio>(responseString.Result);
                return resObject.Id;
            }

            return new Guid();
        }

        public async Task<bool> UpdatePortfolio(Portfolio portfolio)
        {
            string Token = await _localStorageService.GetItemAsync<string>("Token");

            if (portfolio.Id == Guid.Empty || string.IsNullOrEmpty(Token))
                return false;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            var json = JsonConvert.SerializeObject(portfolio);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            //HTTP Put
            var responseTask = _httpClient.PutAsync(_appSettings.BaseAPIUri + "/Portfolios/" + portfolio.Id, stringContent);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }


        public async Task<bool> RemovePortfolio(Portfolio portfolio)
        {
            string Token = await _localStorageService.GetItemAsync<string>("Token");

            if (portfolio.Id == Guid.Empty || string.IsNullOrEmpty(Token))
                return false;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            //HTTP Delete
            var responseTask = _httpClient.DeleteAsync(_appSettings.BaseAPIUri + "/Portfolios/" + portfolio.Id);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}