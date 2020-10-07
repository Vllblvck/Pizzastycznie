using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pizzastycznie.Authentication;
using Pizzastycznie.Authentication.DTO;

namespace Pizzastycznie.Controllers
{
    [Route("api/[controller]")]
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
        [Route(nameof(Register))]
        public ActionResult Register([FromBody] UserRegistrationObject registrationData)
        {
            _logger.LogInformation("Processing user registration");

            var registrationResult = _authService.Register(registrationData);

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
        [Route(nameof(Authenticate))]
        public ActionResult<UserAuthenticationResponseObject> Authenticate([FromBody] UserAuthenticationObject authData)
        {
            var authResult = _authService.Authenticate(authData);

            return authResult switch
            {
                UserAuthenticationResult.Success => TokenGenerator.GenerateToken(authData.Email),
                UserAuthenticationResult.InvalidCredentials => Unauthorized("Incorrect credentials"),
                _ => StatusCode((int) HttpStatusCode.InternalServerError)
            };
        }
    }
}