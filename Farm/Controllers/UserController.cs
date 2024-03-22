using Farm.DTOS;
using Farm.Modelss;
using Farm.Service.IService;
using Farm.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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

        [HttpGet("user-profile/{id}")]
        public IActionResult GetUserProfile(int id)
        {
            var username = _context.Users.FirstOrDefault(x => x.UserId == id);
            if (username == null)
            {
                return NotFound();
            }
            return Ok(username);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProfile(int id, [FromBody] UpdateProfileDTO request)
        {
            try
            {
                _userService.UpdateProfile(id, request);
                return Ok("Profile updated successfully.");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost("change-password/{id}")]
        public IActionResult ChangePassword(int id, ChangePasswordDTO model)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            

            if(user.Password != model.OldPassword)
            {
                return BadRequest("Old Password not match !");
            }else if(user.Password == model.OldPassword)
            {
                user.Password = model.NewPassword;
                _context.SaveChanges();
            }
            return Ok(user);
        }
    }
}
