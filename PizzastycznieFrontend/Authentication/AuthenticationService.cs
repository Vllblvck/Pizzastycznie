using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PizzastycznieFrontend.Authentication.DTO;

namespace PizzastycznieFrontend.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IHttpClientFactory _clientFactory;
        private readonly AppSettings _appSettings;
        private readonly IMemoryCache _cache;

        public AuthenticationService(ILogger<AuthenticationService> logger, IHttpClientFactory clientFactory,
            IOptions<AppSettings> appSettings,
            IMemoryCache cache)
        {
            _logger = logger;
            _clientFactory = clientFactory;
            _appSettings = appSettings.Value;
            _cache = cache;
        }

        public async Task<bool> AuthenticateAsync(UserCredentialsObject userCredentials)
        {
            _logger.LogInformation("Creating http client and preparing request");
            var client = _clientFactory.CreateClient();

            var json = JsonConvert.SerializeObject(userCredentials);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{_appSettings.HostAddress}/api/user/authenticate"),
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            _logger.LogInformation("Sending get token request");
            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                return false;

            var responseJson = await response.Content.ReadAsStringAsync();
            var authObject = JsonConvert.DeserializeObject<UserAuthenticationResponseObject>(responseJson);

            _logger.LogInformation("Saving authentication data in cache");
            _cache.Set(UserDataEnum.UserId, authObject.UserId);
            _cache.Set(UserDataEnum.Email, authObject.Email);
            _cache.Set(UserDataEnum.Token, authObject.Token);
            _cache.Set(UserDataEnum.ExpirationDate, authObject.ExpirationDate);
            _cache.Set(UserDataEnum.PhoneNumber, authObject.PhoneNumber);
            _cache.Set(UserDataEnum.Address, authObject.Address);
            _cache.Set(UserDataEnum.IsAdmin, authObject.IsAdmin);

            return true;
        }
    }
}