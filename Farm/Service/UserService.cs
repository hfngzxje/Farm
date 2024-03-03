using Farm.DTOS;
using Farm.Modelss;
using Farm.Service.IService;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Farm.Services
{
    public class UserService : IUserService
	{
		private readonly FarmContext _context;

        public UserService(FarmContext context)
		{
			_context = context;
        }

        public Task<User> Authenticate(LoginRequestDTO loginRequest)
        {
			User authenticatedUser = _context.Users.SingleOrDefault(u => u.Username == loginRequest.Username && u.Password == loginRequest.Password);

            return Task.FromResult(authenticatedUser);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public void RegisterUser(RegisterRequestDTO registerRequest)
        {
            if (_context.Users.Any(u => u.Username == registerRequest.Username))
            {
                throw new Exception("Username already exists");
            }

            if (_context.Users.Any(u => u.Email == registerRequest.Email))
            {
                throw new Exception("Email already exists");
            }

            if (_context.Users.Any(u => u.Phonenumber == registerRequest.Phonenumber))
            {
                throw new Exception("Phonenumber already exists");
            }

            if (registerRequest.Password.Length < 6)
            {
                throw new Exception("Password must be at least 6 characters long");
            }

            if (!registerRequest.Password.Any(char.IsUpper))
            {
                throw new Exception("Password must contain at least one uppercase letter");
            }

            if (!registerRequest.Password.Any(char.IsDigit))
            {
                throw new Exception("Password must contain at least one digit");
            }

            if (!registerRequest.Password.Any(c => !char.IsLetterOrDigit(c)))
            {
                throw new Exception("Password must contain at least one special character");
            }

            if (registerRequest.Password != registerRequest.Re_Password)
            {
                throw new Exception("Passwords do not match");
            }

            var newUser = new User
            {
                Username = registerRequest.Username,
                Email = registerRequest.Email,
                Phonenumber = registerRequest.Phonenumber,
                Password = registerRequest.Password,
                RoleId = 2,
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();
        }
    }
}

