namespace Farm.DTOS
{
	public class LoginRequestDTO
	{
		public string? Username { get; set; }
		public string? Password { get; set; }
		public List<string> ValidateInput(bool IsUpdate)
		{
			var errors = new List<string>();

			if (String.IsNullOrEmpty(Username))
				errors.Add("Username is required!");
			else if (Username.Length > 250)
				errors.Add("Username of Title must be <= 250");

			if (String.IsNullOrEmpty(Password))
				errors.Add("Password is required!");
			else if (Password.Length > 250)
				errors.Add("Password of Title must be <= 250");


			return errors;
		}
	}
}
