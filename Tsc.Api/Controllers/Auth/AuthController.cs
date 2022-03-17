namespace Tsc.Api.Controllers.Auth
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        /// <summary>
        /// Login endpoint
        /// credentials: test@domain.com - abc123
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public IActionResult Login(AuthConfig model)
        {
            (bool status, string token) = _authService.GenerateToken(model);
            if (!status)
                return BadRequest("Invalid credentials");

            return Ok(token);
        }
    }
}
