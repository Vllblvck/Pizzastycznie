using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PizzastycznieFrontend.ApiHandler.DTO;
using PizzastycznieFrontend.ApiHandler.DTO.Enums;

namespace PizzastycznieFrontend.ApiHandler
{
    public class ApiHandler : IApiHandler
    {
        private readonly ILogger<ApiHandler> _logger;
        private readonly AppSettings _appSettings;
        private readonly IHttpClientFactory _clientFactory;

        public ApiHandler(ILogger<ApiHandler> logger, IOptions<AppSettings> options, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _appSettings = options.Value;
            _clientFactory = clientFactory;
        }

        public async Task<IEnumerable<Food>> GetMenuItemsAsync()
        {
            _logger.LogInformation("Sending get request to retrieve menu items");

            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_appSettings.HostAddress}/api/food/getall");
            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode) return null;

            _logger.LogInformation("Deserializing json response");

            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Food>>(jsonResponse);
        }

        public async Task<AddMenuItemResult> AddMenuItemAsync(Food food)
        {
            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_appSettings.HostAddress}/api/food/add");
            var response = await client.SendAsync(request);

            return response.StatusCode switch
            {
                HttpStatusCode.Created => AddMenuItemResult.Success,
                HttpStatusCode.Conflict => AddMenuItemResult.Conflict,
                HttpStatusCode.InternalServerError => AddMenuItemResult.ServerError,
                _ => AddMenuItemResult.ServerError
            };
        }

        public Task DeleteMenuItemAsync(string foodName)
        {
            throw new System.NotImplementedException();
        }
    }
}