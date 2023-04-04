using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UniversityManagementAPIApp.DTOs;
using UniversityManagementAPIApp.DTOs.CreateUpdateObjects;
using UniversityManagementAPIApp.DTOs.PatchObjects;
using UniversityManagementAPIApp.Helpers;
using UniversityManagementAPIApp.Models;
using UniversityManagementAPIApp.Services;

namespace UniversityManagementAPIApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

	public class TeachersController : ControllerBase
	{
		private readonly ITeachersService _teachersService;

		private readonly ILogger<TeachersController> _logger;

		public TeachersController(ITeachersService teachersService, ILogger<TeachersController> logger)
		{
			_teachersService = teachersService;
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllAsync()
		{
			try
			{
				_logger.LogInformation("GetTeachers started");
				var teachers = await _teachersService.GetTeachersAsync();
				if (teachers == null || !teachers.Any())
				{
					return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
				}

				return Ok(teachers);
			}
			catch (Exception ex)
			{
				_logger.LogError($"GetAllTeachers error: {ex.Message}");
				return StatusCode((int)(HttpStatusCode.InternalServerError), ex.Message);
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetTeacherAsync([FromRoute] Guid id)
		{
			try
			{
				_logger.LogInformation("GetTeachers started");
				var teacher = await _teachersService.GetTeacherByIdAsync(id);
				if (teacher == null)
				{
					return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
				}
				return Ok(teacher);
			}
			catch (Exception ex)
			{
				_logger.LogError($"GetTeacherById error: {ex.Message}");
				return StatusCode((int)(HttpStatusCode.InternalServerError), ex.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> PostTeacherAsync([FromBody] Teacher teacher)
		{
			try
			{
				_logger.LogInformation("CreateTeacherAsync started");
				if (teacher == null)
				{
					return BadRequest(ErrorMessagesEnum.BadRequest);
				}
				await _teachersService.CreateTeacherAsync(teacher);
				return Ok(SuccessMessagesEnum.ElementSuccesfullyAdded);
			}
			catch (ModelValidationException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Validation exception {ex.Message}");
				return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteTeacherAsync([FromRoute] Guid id)
		{
			try
			{
				_logger.LogInformation("DeleteTeacherAsync started");
				bool result = await _teachersService.DeleteTeacherAsync(id);
				if (result)
				{
					return Ok(SuccessMessagesEnum.ElementSuccesfullyDeleted);
				}
				return BadRequest(ErrorMessagesEnum.NoElementFound);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Validation exception {ex.Message}");
				return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> PutTeacher([FromRoute] Guid id, [FromBody] CreateUpdateTeacher teacher)
		{
			try
			{
				_logger.LogInformation("Update started");
				if (teacher == null)
				{
					return BadRequest(ErrorMessagesEnum.BadRequest);
				}
				CreateUpdateTeacher updatedTeacher = await _teachersService.UpdateTeacherAsync(id, teacher);
				if (updatedTeacher == null)
				{
					return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
				}
				return Ok(SuccessMessagesEnum.ElementSuccesfullyUpdated);
			}
			catch (ModelValidationException ex)
			{
				_logger.LogError($"Validation exception {ex.Message}");
				return BadRequest(ex.Message);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Validation exception {ex.Message}");
				return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
			}
		}

		[HttpPatch("{id}")]
		public async Task<IActionResult> PatchTeacher([FromRoute] Guid id, [FromBody] PatchTeacher teacher)
		{
			try
			{
				_logger.LogInformation("Update started");
				if (teacher == null)
				{
					return BadRequest(ErrorMessagesEnum.BadRequest);
				}
				PatchTeacher updatedTeacher = await _teachersService.UpdatePartiallyTeacherAsync(id, teacher);
				if (updatedTeacher == null)
				{
					return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
				}
				return Ok(SuccessMessagesEnum.ElementSuccesfullyUpdated);
			}
			catch (ModelValidationException ex)
			{
				_logger.LogError($"Validation exception {ex.Message}");
				return BadRequest(ex.Message);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Validation exception {ex.Message}");
				return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
			}
		}
	}
}
