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

	public class TeacherTypesController : ControllerBase
	{
		private readonly ITeacherTypesService _teacherTypesService;

		private readonly ILogger<TeacherTypesController> _logger;

		public TeacherTypesController(ITeacherTypesService teacherTypesService, ILogger<TeacherTypesController> logger)
		{
			_teacherTypesService = teacherTypesService;
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllAsync()
		{
			try
			{
				_logger.LogInformation("GetTeacherTypes started");
				var teacherTypes = await _teacherTypesService.GetTeacherTypesAsync();
				if (teacherTypes == null || !teacherTypes.Any())
				{
					return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
				}

				return Ok(teacherTypes);
			}
			catch (Exception ex)
			{
				_logger.LogError($"GetAllTeacherTypes error: {ex.Message}");
				return StatusCode((int)(HttpStatusCode.InternalServerError), ex.Message);
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetTeacherTypeAsync([FromRoute] Guid id)
		{
			try
			{
				_logger.LogInformation("GetTeacherTypes started");
				var teacherType = await _teacherTypesService.GetTeacherTypeByIdAsync(id);
				if (teacherType == null)
				{
					return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
				}
				return Ok(teacherType);
			}
			catch (Exception ex)
			{
				_logger.LogError($"GetTeacherTypeById error: {ex.Message}");
				return StatusCode((int)(HttpStatusCode.InternalServerError), ex.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> PostTeacherTypeAsync([FromBody] TeacherType teacherType)
		{
			try
			{
				_logger.LogInformation("CreateTeacherTypeAsync started");
				if (teacherType == null)
				{
					return BadRequest(ErrorMessagesEnum.BadRequest);
				}
				await _teacherTypesService.CreateTeacherTypeAsync(teacherType);
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
		public async Task<IActionResult> DeleteTeacherTypeAsync([FromRoute] Guid id)
		{
			try
			{
				_logger.LogInformation("DeleteTeacherTypeAsync started");
				bool result = await _teacherTypesService.DeleteTeacherTypeAsync(id);
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
		public async Task<IActionResult> PutTeacherType([FromRoute] Guid id, [FromBody] CreateUpdateTeacherType teacherType)
		{
			try
			{
				_logger.LogInformation("Update started");
				if (teacherType == null)
				{
					return BadRequest(ErrorMessagesEnum.BadRequest);
				}
				CreateUpdateTeacherType updatedTeacherType = await _teacherTypesService.UpdateTeacherTypeAsync(id, teacherType);
				if (updatedTeacherType == null)
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
		public async Task<IActionResult> PatchTeacherType([FromRoute] Guid id, [FromBody] PatchTeacherType teacherType)
		{
			try
			{
				_logger.LogInformation("Update started");
				if (teacherType == null)
				{
					return BadRequest(ErrorMessagesEnum.BadRequest);
				}
				PatchTeacherType updatedTeacherType = await _teacherTypesService.UpdatePartiallyTeacherTypeAsync(id, teacherType);
				if (updatedTeacherType == null)
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
