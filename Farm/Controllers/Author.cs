using Farm.Modelss;
using Farm.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Farm.Controllers
{
    [ApiController]
    public class Author : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly FarmContext _farmContext;

        public Author(IUserService userService, FarmContext farmContext)
        {
            _userService = userService;
            _farmContext = farmContext;
        }

        [HttpGet("Clients/Home")]
        public IActionResult Home()
        {
            return Ok();
        }

        [HttpGet("Admin/Produce")]
        public IActionResult AdminHome()
        {
            var username = HttpContext.Session.GetString("Username");
            var user = _farmContext.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                var role = user.RoleId;
                return Ok(role);
            }            
        }
    }
}
