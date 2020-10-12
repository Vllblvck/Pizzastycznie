using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pizzastycznie.Authentication.DTO;
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

            //TODO Send email

            return result switch
            {
                true => StatusCode((int) HttpStatusCode.Created),
                false => StatusCode((int) HttpStatusCode.InternalServerError)
            };
        }

        [HttpPost]
        [Authorize(Roles = UserRole.Admin)]
        public async Task ChangeStatus()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Authorize(Roles = UserRole.Admin)]
        public async Task<IEnumerable<Order>> GetAllPending()
        {
            _logger.LogInformation("Selecting all pending orders from database");

            return await _orderRepository.SelectAllPendingOrdersAsync();
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllForUser(string email)
        {
            _logger.LogInformation("Extracting user email and role from token");

            var emailFromToken = User.Claims
                .Where(c => c.Type == ClaimTypes.Name)
                .Select(c => c.Value)
                .FirstOrDefault();

            var userRole = User.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .FirstOrDefault();

            _logger.LogInformation("Checking if user has permission for resource");
            if (!emailFromToken.Equals(email) && userRole != UserRole.Admin)
                return Forbid();

            _logger.LogInformation("Returning orders history");
            return new ActionResult<IEnumerable<Order>>(await _orderRepository.SelectAllOrdersForUserAsync(email));
        }
    }
}