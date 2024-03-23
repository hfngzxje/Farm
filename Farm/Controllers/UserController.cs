using Farm.DTOS;
using Farm.Modelss;
using Farm.Service.IService;
using Farm.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

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
            int roleId = authenticatedUser.RoleId ?? 0;
            HttpContext.Session.SetInt32("RoleId", roleId);



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


        [HttpPost("forgotPassword/{email}")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return NotFound("User not found");
            }

            // Generate a random password
            var newPassword = GenerateRandomPassword();


            user.Password = newPassword;
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Failed to update user's password");
            }

            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential("buiduchung300802@gmail.com", "szwl dfpp ozyz iaez"),
                    Port = 587,
                    EnableSsl = true
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("buiduchung300802@gmail.com"),
                    Subject = "New Password",
                    Body = $"Your new password is: {newPassword}",
                    IsBodyHtml = true
                };

                mailMessage.To.Add(email);

                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception)
            {
                return StatusCode(500, "Failed to send email");
            }

            return Ok("New password sent to your email");
        }

        private string GenerateRandomPassword(int length = 8)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            var random = new Random();
            var password = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                password.Append(validChars[random.Next(validChars.Length)]);
            }
            return password.ToString();
        }

    }
}
