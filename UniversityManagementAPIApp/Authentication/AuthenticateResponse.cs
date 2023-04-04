using UniversityManagementAPIApp.DTOs;

namespace UniversityManagementAPIApp.Authentication
{
	public class AuthenticateResponse
	{
		public Guid IdTeacher { get; set; }
		public string Name { get; set; }
		public string Token { get; set; }

		public AuthenticateResponse(Teacher user, string token)
		{
			IdTeacher = user.IdTeacher;
			Name = user.Name;
			Token = token;
		}
	}
}
