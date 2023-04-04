using AutoMapper.Execution;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using UniversityManagementAPIApp.Authentication;
using UniversityManagementAPIApp.DataContext;
using UniversityManagementAPIApp.DTOs;

namespace UniversityManagementAPIApp.Services.UserService
{
	public class UserService : IUserService
	{
		private readonly UniversityManagementDataContext _context;
		private readonly IConfiguration _configuration;

		public UserService(UniversityManagementDataContext context, IConfiguration configuration)
		{
			_context = context;
			_configuration = configuration;
		}

		public AuthenticateResponse Authenticate(AuthenticateRequest model)
		{
			var user = _context.Teachers.SingleOrDefault(x => x.IdTeacher == model.Username && x.Name == model.Password);

			//return null if user not found
			if (user == null)
			{
				return null;
			}

			//authentication successfull so generate jwt token
			var token = generateJwtToken(user);

			return new AuthenticateResponse(user, token);
		}

		//helper methods
		private string generateJwtToken(Teacher user)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Authentication:Secret")));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
			var token = new JwtSecurityToken("http://localhost:7154", "http://localhost:7154", null,
				expires: DateTime.Now.AddDays(3), signingCredentials: credentials);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
