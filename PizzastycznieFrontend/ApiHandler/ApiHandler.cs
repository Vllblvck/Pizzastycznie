using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PizzastycznieFrontend.ApiHandler.DTO;
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
                new AuthenticationHeaderValue("Bearer", (string) _cache.Get(UserDataEnum.Token));

            var response = await client.SendAsync(request);

            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<Order>> GetOrderHistoryAsync(string email)
        {
            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_appSettings.HostAddress}/api/order/all?email={email}")
            };
            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", (string) _cache.Get(UserDataEnum.Token));

            var response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                return null;

            var responseJson = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Order>>(responseJson);
        }
    }
}