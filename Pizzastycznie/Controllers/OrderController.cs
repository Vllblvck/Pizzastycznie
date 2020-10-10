using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pizzastycznie.Database.DTO;
using Pizzastycznie.Database.Repositories.Interfaces;

namespace Pizzastycznie.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderRepository _orderRepository;

        public OrderController(ILogger<OrderController> logger, IOrderRepository orderRepository)
        {
            _logger = logger;
            _orderRepository = orderRepository;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Create([FromBody] Order order)
        {
            _logger.LogInformation("Creating order");
            var result = await _orderRepository.InsertOrderAsync(order);

            _logger.LogInformation("Sending response back to client");
            return result switch
            {
                true => StatusCode((int) HttpStatusCode.Created),
                false => StatusCode((int) HttpStatusCode.InternalServerError)
            };
        }

        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<Order>> GetAll()
        {
            _logger.LogInformation("Extracting user email from token");

            var email = User.Claims
                .Where(c => c.Type == ClaimTypes.Name)
                .Select(c => c.Value)
                .FirstOrDefault();

            _logger.LogInformation("Sending response");

            return await _orderRepository.SelectAllOrdersAsync(email);
        }
    }
}