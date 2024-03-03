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
            // Xóa session
            HttpContext.Session.Clear();

            // Xóa sessionStorage
            return Ok(new { message = "Logout successful" });
        }


    }
}
