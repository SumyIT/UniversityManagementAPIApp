﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementAPIApp.Authentication;
using UniversityManagementAPIApp.Services.UserService;

namespace UniversityManagementAPIApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private IUserService _userService;
		public AuthController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpPost("authenticate")]
		public IActionResult Authenticate(AuthenticateRequest model)
		{
			var response = _userService.Authenticate(model);

			if (response == null)
			{
				return BadRequest(new { message = "Username or password is incorrect" });
			}

			return Ok(response);
		}
	}
}
