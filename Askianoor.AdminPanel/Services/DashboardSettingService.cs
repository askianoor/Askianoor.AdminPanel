using Askianoor.AdminPanel.Data.Models;
using Askianoor.AdminPanel.Services;
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
    public class DashboardSettingService
    {
        private readonly ISessionStorageService _localStorageService;
        private readonly ApplicationSettings _appSettings;
        private readonly HttpClient _httpClient;

        public DashboardSettingService(ISessionStorageService localStorageService, IOptions<ApplicationSettings> appSettings)
        {
            _localStorageService = localStorageService;
            _appSettings = appSettings.Value;
            _httpClient = new HttpClient();
        }

        public async Task<DashboardSetting> GetDashboardSettings()
        {
            //string Token = await _localStorageService.GetItemAsync<string>("Token");

            //if (string.IsNullOrEmpty(Token))
            //    return null;

            //DashboardSetting dashboardSetting = new DashboardSetting() { Id = 1 };

            //using (var client = new HttpClient())
            //{
            //    //var json = JsonConvert.SerializeObject(body);
            //    //var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            //    //HTTP GET
            //    var responseTask = client.GetAsync(_appSettings.BaseAPIUri + "/DashboardSettings/1"+ dashboardSetting.Id);
            //    responseTask.Wait();

            //    var result = responseTask.Result;
            //    if (result.IsSuccessStatusCode)
            //    {
            //        var responseString = result.Content.ReadAsStringAsync();
            //        return JsonConvert.DeserializeObject<DashboardSetting>(responseString.Result);
            //    }
            //}
            //return null;


            try
            {
                string token = await _localStorageService.GetItemAsync<string>("Token");
                return await _httpClient.GetJsonAsync<DashboardSetting, string>(_appSettings.BaseAPIUri + "/DashboardSettings/1", token, null).ConfigureAwait(true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DashboardSetting> AddDashboardSetting(DashboardSetting dashboardSetting)
        {
            //string Token = await _localStorageService.GetItemAsync<string>("Token");

            //if (string.IsNullOrEmpty(Token))
            //    return 0;

            ////var body = new { dashboardSetting , Token };

            //using (var client = new HttpClient())
            //{
            //    var json = JsonConvert.SerializeObject(dashboardSetting);
            //    var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            //    //HTTP Post
            //    var responseTask = client.PostAsync(_appSettings.BaseAPIUri + "/DashboardSettings", stringContent);
            //    responseTask.Wait();

            //    var result = responseTask.Result;
            //    if (result.IsSuccessStatusCode)
            //    {
            //        var responseString = result.Content.ReadAsStringAsync();
            //        var resObject = JsonConvert.DeserializeObject<DashboardSetting>(responseString.Result);
            //        return resObject.Id;
            //    }
            //}
            //return 0;

            try
            {
                string token = await _localStorageService.GetItemAsync<string>("Token");
                return await _httpClient.PostJsonAsync<DashboardSetting, DashboardSetting>(_appSettings.BaseAPIUri + "/DashboardSettings", token, dashboardSetting).ConfigureAwait(true);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<bool> UpdateDashboardSetting(DashboardSetting dashboardSetting)
        {
            //string Token = await _localStorageService.GetItemAsync<string>("Token");

            //if (string.IsNullOrEmpty(Token) || dashboardSetting.Id == 0)
            //    return false;

            //using (var client = new HttpClient())
            //{
            //    var json = JsonConvert.SerializeObject(dashboardSetting);
            //    var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            //    //HTTP Post
            //    var responseTask = client.PutAsync(_appSettings.BaseAPIUri + "/DashboardSettings/" + dashboardSetting.Id, stringContent);
            //    responseTask.Wait();

            //    var result = responseTask.Result;
            //    if (result.IsSuccessStatusCode)
            //    {
            //        return true;
            //    }
            //}
            //return false;

            try
            {
                string token = await _localStorageService.GetItemAsync<string>("Token");
                return await _httpClient.PutJsonAsync<DashboardSetting>(_appSettings.BaseAPIUri + "/DashboardSettings/" + dashboardSetting.Id, token, dashboardSetting).ConfigureAwait(true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> RemoveDashboardSetting(DashboardSetting dashboardSetting)
        {
            //string Token = await _localStorageService.GetItemAsync<string>("Token");

            //if (string.IsNullOrEmpty(Token) || dashboardSetting.Id == 0)
            //    return false;

            //using (var client = new HttpClient())
            //{
            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            //    //HTTP Delete
            //    var responseTask = client.DeleteAsync(_appSettings.BaseAPIUri + "/DashboardSettings/" + dashboardSetting.Id);
            //    responseTask.Wait();

            //    var result = responseTask.Result;
            //    if (result.IsSuccessStatusCode)
            //    {
            //        return true;
            //    }
            //}
            //return false;


            try
            {
                string token = await _localStorageService.GetItemAsync<string>("Token");
                return await _httpClient.DeleteJsonAsync<DashboardSetting>(_appSettings.BaseAPIUri + "/DashboardSettings/" + dashboardSetting.Id, token).ConfigureAwait(true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
