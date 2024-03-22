using Farm.DTOS;
using Farm.Modelss;

namespace Farm.Service.IService
{
	public interface IUserService
	{
        Task<User> Authenticate(LoginRequestDTO loginRequest);
        Task<User> GetUserByUsernameAsync(string username);

        void RegisterUser(RegisterRequestDTO registerRequest);

		Task<UserDTO> GetUserByIdAsync(int userId);

        void UpdateProfile(int id, UpdateProfileDTO request);
    }
}
