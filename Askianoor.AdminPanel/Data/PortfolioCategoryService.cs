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
    public class PortfolioCategoryService
    {
        private readonly HttpClient _httpClient;
        private readonly ApplicationSettings _appSettings;
        private readonly ISessionStorageService _localStorageService;

        public PortfolioCategoryService(IOptions<ApplicationSettings> appSettings, ISessionStorageService localStorageService)
        {
            _appSettings = appSettings.Value;
            _localStorageService = localStorageService;
            _httpClient = new HttpClient();
        }

        public List<PortfolioCategory> GetPortfolioCategorys()
        {
            //HTTP GET
            var responseTask = _httpClient.GetAsync(_appSettings.BaseAPIUri + "/PortfolioCategories");
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var responseString = result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<PortfolioCategory>>(responseString.Result);
            }

            return null;
        }


        public async Task<Guid> AddPortfolioCategory(PortfolioCategory portfolioCategory)
        {
            string Token = await _localStorageService.GetItemAsync<string>("Token");

            if (string.IsNullOrEmpty(Token))
                return new Guid();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            var json = JsonConvert.SerializeObject(portfolioCategory);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            //HTTP Post
            var responseTask = _httpClient.PostAsync(_appSettings.BaseAPIUri + "/PortfolioCategories", stringContent);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var responseString = result.Content.ReadAsStringAsync();
                var resObject = JsonConvert.DeserializeObject<PortfolioCategory>(responseString.Result);
                return resObject.PortfolioCategoryId;
            }

            return new Guid();
        }

        public async Task<bool> UpdatePortfolioCategory(PortfolioCategory portfolioCategory)
        {
            string Token = await _localStorageService.GetItemAsync<string>("Token");

            if (portfolioCategory.PortfolioCategoryId == Guid.Empty || string.IsNullOrEmpty(Token))
                return false;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            var json = JsonConvert.SerializeObject(portfolioCategory);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            //HTTP Put
            var responseTask = _httpClient.PutAsync(_appSettings.BaseAPIUri + "/PortfolioCategories/" + portfolioCategory.PortfolioCategoryId, stringContent);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }


        public async Task<bool> RemovePortfolioCategory(PortfolioCategory portfolioCategory)
        {
            string Token = await _localStorageService.GetItemAsync<string>("Token");

            if (portfolioCategory.PortfolioCategoryId == Guid.Empty || string.IsNullOrEmpty(Token))
                return false;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            //HTTP Delete
            var responseTask = _httpClient.DeleteAsync(_appSettings.BaseAPIUri + "/PortfolioCategories/" + portfolioCategory.PortfolioCategoryId);
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
