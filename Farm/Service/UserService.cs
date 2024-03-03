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
    }
}
