using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly ITokenHandler tokenHandler;

        public AuthController(IUserRepository userRepository, ITokenHandler tokenHandler)
        {
            this.userRepository = userRepository;
            this.tokenHandler = tokenHandler;
        }

        [HttpPost]
        [Route("login")]

        public async Task<IActionResult> LoginAsync(Models.DTO.LoginRequest loginRequest)
        {
            // Validate the incoming request           
            if (!await ValidateLoginAsync(loginRequest))
            {
                return BadRequest(ModelState);
            }

            // Check if user is authenticated

            // Check username and password
            var user = await userRepository.AuthenticateAsync(
                loginRequest.Username, loginRequest.Password);

            if (user != null)
            {
                // Generate a Jwt token
                var token = await tokenHandler.CreateTokenAsync(user);
                return Ok(token);
            }

            return BadRequest("Username or password incorrect");
        }

        #region Private Methods

        private async Task<bool> ValidateLoginAsync(Models.DTO.LoginRequest loginRequest)
        {
            if (string.IsNullOrWhiteSpace(loginRequest.Username))
            {
                ModelState.AddModelError(nameof(LoginRequest.Username),
                    $"{nameof(LoginRequest.Username)} cannot be null or empty or white space.");
            }

            if (string.IsNullOrWhiteSpace(loginRequest.Password))
            {
                ModelState.AddModelError(nameof(LoginRequest.Password),
                    $"{nameof(LoginRequest.Password)} cannot be null or empty or white space.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
