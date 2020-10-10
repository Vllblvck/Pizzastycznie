using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pizzastycznie.Authentication;
using Pizzastycznie.Authentication.DTO;

namespace Pizzastycznie.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserAuthenticationService _authService;

        public UserController(ILogger<UserController> logger, IUserAuthenticationService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        [HttpPost]
        public async Task<ActionResult> Register([FromBody] UserRegistrationObject registrationData)
        {
            _logger.LogInformation("Processing user registration");

            var registrationResult = await _authService.RegisterAsync(registrationData);

            _logger.LogInformation("Sending registration response");

            return registrationResult switch
            {
                UserRegistrationResult.Success => StatusCode((int) HttpStatusCode.Created),
                UserRegistrationResult.UserExists => BadRequest(
                    $"User with email {registrationData.Email} already exists"),
                UserRegistrationResult.BadEmail => BadRequest("Invalid email address"),
                UserRegistrationResult.BadPassword => BadRequest("Password requirements not met"),
                UserRegistrationResult.DatabaseError => StatusCode((int) HttpStatusCode.InternalServerError),
                _ => StatusCode((int) HttpStatusCode.InternalServerError)
            };
        }

        [HttpPost]
        public async Task<ActionResult<UserAuthenticationResponseObject>> Authenticate(
            [FromBody] UserAuthenticationObject authData)
        {
            _logger.LogInformation("Processing user authentication");

            var user = await _authService.AuthenticateAsync(authData);

            _logger.LogInformation("Sending authentication response");

            if (user == null)
                return Unauthorized("Incorrect credentials");

            return user;
        }
    }
}
