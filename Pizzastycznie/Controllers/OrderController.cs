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
using Pizzastycznie.Mail;

namespace Pizzastycznie.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderRepository _orderRepository;
        private readonly IMailService _mailService;

        public OrderController(ILogger<OrderController> logger, IOrderRepository orderRepository,
            IMailService mailService)
        {
            _logger = logger;
            _orderRepository = orderRepository;
            _mailService = mailService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] Order order)
        {
            _logger.LogInformation("Creating order");
            var result = await _orderRepository.InsertOrderAsync(order);

            if (!result) return StatusCode((int) HttpStatusCode.InternalServerError);

            _logger.LogInformation("Extracting user email from token");
            var userEmail = User.Claims
                .Where(c => c.Type == ClaimTypes.Name)
                .Select(c => c.Value)
                .FirstOrDefault();

            _logger.LogInformation("Sending email to user");
            await _mailService.SendEmailAsync(userEmail, "", "");

            return StatusCode((int) HttpStatusCode.Created);
        }

        [HttpPut]
        [Authorize(Roles = UserRole.Admin)]
        public async Task<IActionResult> Update(long orderId, string status)
        {
            _logger.LogInformation("Changing order status");

            var result = await _orderRepository.UpdateOrderStatusAsync(orderId, status);

            return result switch
            {
                true => Ok(),
                false => StatusCode((int) HttpStatusCode.InternalServerError)
            };
        }

        [HttpGet]
        [Authorize(Roles = UserRole.Admin)]
        public async Task<IEnumerable<Order>> Pending()
        {
            _logger.LogInformation("Selecting all pending orders from database");

            return await _orderRepository.SelectPendingOrdersAsync();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> All(string email)
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

            var result = await _orderRepository.SelectOrdersForUserAsync(email);

            return Ok(result);
        }
    }
}