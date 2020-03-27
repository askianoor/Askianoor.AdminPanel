using Blazored.SessionStorage;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Askianoor.AdminPanel.Data
{
    public class GeneralService
    {
        private readonly ISessionStorageService _localStorageService;
        private readonly ApplicationSettings _appSettings;

        public GeneralService(ISessionStorageService localStorageService, IOptions<ApplicationSettings> appSettings)
        {
            _localStorageService = localStorageService;
            _appSettings = appSettings.Value;
        }

        public async Task<string> GetStringToken()
        {
            string Token = await _localStorageService.GetItemAsync<string>("Token");
            return Token;
        }

        public string GetStringAPI()
        {
            return  _appSettings.BaseAPIUri;
        }

    }
}
