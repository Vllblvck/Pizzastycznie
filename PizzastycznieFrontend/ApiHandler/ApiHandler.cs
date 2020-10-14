using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PizzastycznieFrontend.ApiHandler.DTO;
using PizzastycznieFrontend.ApiHandler.DTO.Enums;
using PizzastycznieFrontend.Authentication;

namespace PizzastycznieFrontend.ApiHandler
{
    public class ApiHandler : IApiHandler
    {
        private readonly ILogger<ApiHandler> _logger;
        private readonly AppSettings _appSettings;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IMemoryCache _cache;

        public ApiHandler(ILogger<ApiHandler> logger, IOptions<AppSettings> options, IHttpClientFactory clientFactory,
            IMemoryCache cache)
        {
            _logger = logger;
            _appSettings = options.Value;
            _clientFactory = clientFactory;
            _cache = cache;
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

        public async Task<bool> PlaceOrderAsync(Order order)
        {
            var client = _clientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(order);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{_appSettings.HostAddress}/api/order/create"),
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", (string) _cache.Get(AuthenticationDataEnum.Token));

            var response = await client.SendAsync(request);

            return response.IsSuccessStatusCode;
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