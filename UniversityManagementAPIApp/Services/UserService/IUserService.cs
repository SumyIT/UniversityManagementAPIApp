using UniversityManagementAPIApp.Authentication;

namespace UniversityManagementAPIApp.Services.UserService
{
	public interface IUserService
	{
		AuthenticateResponse Authenticate(AuthenticateRequest model);
	}
}
