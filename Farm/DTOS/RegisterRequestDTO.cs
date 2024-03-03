namespace Farm.DTOS
{
	public class RegisterRequestDTO
	{
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Phonenumber { get; set; }
        public string? Password { get; set; }
        public string? Re_Password { get; set; }
    }
}
