using System.ComponentModel.DataAnnotations;

namespace UniversityManagementAPIApp.Authentication
{
	public class AuthenticateRequest
	{
		[Required]
		public Guid Username { get; set; }   //Name - tabela Teacher

		[Required]
		public string Password { get; set; }   //IdTeacher - tabela Teacher
	}
}
