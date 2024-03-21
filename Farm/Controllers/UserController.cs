using Farm.DTOS;
using Farm.Modelss;
using Farm.Service.IService;
using Farm.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Farm.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly FarmContext _context;

		public UserController(IUserService userService, IHttpContextAccessor httpContextAccessor, FarmContext context)
		{
			_userService = userService;
            _httpContextAccessor = httpContextAccessor;
			_context = context;

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

        [HttpGet("isAdmin")]
        public IActionResult IsAdmin()
        {
            try
            {
                var username = HttpContext.Session.GetString("Username");
                if (!string.IsNullOrEmpty(username))
                {
                    var user = _context.Users.FirstOrDefault(u => u.Username == username);

                    if (user != null)
                    {
                        return Ok(user.RoleId);
                    }
                }

                return NotFound(); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

		[HttpGet("{userId}")]
		public async Task<ActionResult<UserDTO>> GetUserById(int userId)
		{
			var user = await _userService.GetUserByIdAsync(userId);
			if (user == null)
			{
				return NotFound();
			}
			return Ok(user);
		}
	}
}
