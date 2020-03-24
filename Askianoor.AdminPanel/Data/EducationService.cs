using Askianoor.AdminPanel.Data.Models;
using Blazored.SessionStorage;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Askianoor.AdminPanel.Data
{
    public class EducationService
    {
        private readonly ISessionStorageService _localStorageService;
        private readonly ApplicationSettings _appSettings;

        public EducationService(ISessionStorageService localStorageService, IOptions<ApplicationSettings> appSettings)
        {
            _localStorageService = localStorageService;
            _appSettings = appSettings.Value;
        }

        public async Task<List<Education>> GetEducations()
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
                var responseTask = client.GetAsync(_appSettings.BaseAPIUri + "/Educations");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var responseString = result.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Education>>(responseString.Result);
                }
            }
            return null;
        }


        public async Task<Guid> AddEducation(Education education)
        {
            string Token = await _localStorageService.GetItemAsync<string>("Token");

            if (string.IsNullOrEmpty(Token))
                return new Guid();

            //var body = new { education , Token };

            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(education);
                var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                //HTTP Post
                var responseTask = client.PostAsync(_appSettings.BaseAPIUri + "/Educations", stringContent);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var responseString = result.Content.ReadAsStringAsync();
                    var resObject = JsonConvert.DeserializeObject<Education>(responseString.Result);
                    return resObject.EducationId;
                }
            }
            return new Guid();
        }

        public async Task<bool> UpdateEducation(Education education)
        {
            string Token = await _localStorageService.GetItemAsync<string>("Token");

            if (string.IsNullOrEmpty(Token) || education.EducationId == Guid.Empty)
                return false;

            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(education);
                var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                //HTTP Post
                var responseTask = client.PutAsync(_appSettings.BaseAPIUri + "/Educations/" + education.EducationId, stringContent);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }


        public async Task<bool> RemoveEducation(Education education)
        {
            string Token = await _localStorageService.GetItemAsync<string>("Token");

            if (string.IsNullOrEmpty(Token) || education.EducationId == Guid.Empty)
                return false;

            using (var client = new HttpClient())
            {
                //HTTP Delete
                var responseTask = client.DeleteAsync(_appSettings.BaseAPIUri + "/Educations/" + education.EducationId);
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
