using Farm.DTOS;
using Farm.Modelss;
using Farm.Service.IService;
using Farm.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Farm.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(IUserService userService, IHttpContextAccessor httpContextAccessor)
		{
			_userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginRequestDTO loginRequest)
        {

            var authenticatedUser = await _userService.Authenticate(loginRequest);

            if (authenticatedUser == null)
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

            HttpContext.Session.SetString("Username", authenticatedUser.Username);


            var user = await _userService.GetUserByUsernameAsync(authenticatedUser.Username);

            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            return Ok(new { message = "Authentication successful", user });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return Ok(new { message = "Logout successful" });
        }


        [HttpPost("register")]
        public IActionResult Register(RegisterRequestDTO registerRequest)
        {
            try
            {
                _userService.RegisterUser(registerRequest);
                return Ok("User registered successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
