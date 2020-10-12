using System.Collections.Generic;
using System.Net;
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
    public class FoodController : ControllerBase
    {
        private readonly ILogger<FoodController> _logger;
        private readonly IFoodRepository _foodRepository;

        public FoodController(ILogger<FoodController> logger, IFoodRepository foodRepository)
        {
            _logger = logger;
            _foodRepository = foodRepository;
        }

        [HttpPost]
        [Authorize(Roles = UserRole.Admin)]
        public async Task<ActionResult> Add([FromBody] Food food)
        {
            _logger.LogInformation("Inserting food to database");
            var result = await _foodRepository.InsertFoodAsync(food);

            _logger.LogInformation("Sending insert food result");
            return result switch
            {
                true => StatusCode((int) HttpStatusCode.Created),
                false => StatusCode((int) HttpStatusCode.InternalServerError)
            };
        }

        [HttpGet]
        public async Task<IEnumerable<Food>> GetAll()
        {
            _logger.LogInformation("Selecting all food from database");
            return await _foodRepository.SelectAllFoodAsync();
        }

        [HttpDelete]
        [Authorize(Roles = UserRole.Admin)]
        public async Task<ActionResult> Delete(string foodName)
        {
            _logger.LogInformation("Deleting food from database");

            var result = await _foodRepository.DeleteFoodAsync(foodName);

            _logger.LogInformation("Sending delete food result");

            return result switch
            {
                true => Ok("Food was successfully deleted"),
                false => StatusCode((int) HttpStatusCode.InternalServerError)
            };
        }
    }
}