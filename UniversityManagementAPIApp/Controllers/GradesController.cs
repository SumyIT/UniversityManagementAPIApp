using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UniversityManagementAPIApp.Services;
using UniversityManagementAPIApp.DTOs;
using UniversityManagementAPIApp.DTOs.CreateUpdateObjects;
using UniversityManagementAPIApp.DTOs.PatchObjects;
using UniversityManagementAPIApp.Helpers;
using UniversityManagementAPIApp.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace UniversityManagementAPIApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class GradesController : ControllerBase
	{
		private readonly IGradesService _gradesService;

		private readonly ILogger<GradesController> _logger;

		public GradesController(IGradesService gradesService, ILogger<GradesController> logger)
		{
			_gradesService = gradesService;
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllAsync()
		{
			try
			{
				_logger.LogInformation("GetGrades started");
				var grades = await _gradesService.GetGradesAsync();
				if (grades == null || !grades.Any())
				{
					return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
				}

				return Ok(grades);
			}
			catch (Exception ex)
			{
				_logger.LogError($"GetAllGrades error: {ex.Message}");
				return StatusCode((int)(HttpStatusCode.InternalServerError), ex.Message);
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetGradeAsync([FromRoute] Guid id)
		{
			try
			{
				_logger.LogInformation("GetGrades started");
				var grade = await _gradesService.GetGradeByIdAsync(id);
				if (grade == null)
				{
					return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
				}
				return Ok(grade);
			}
			catch (Exception ex)
			{
				_logger.LogError($"GetGradeById error: {ex.Message}");
				return StatusCode((int)(HttpStatusCode.InternalServerError), ex.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> PostGradeAsync([FromBody] Grade grade)
		{
			try
			{
				_logger.LogInformation("CreateGradeAsync started");
				if (grade == null)
				{
					return BadRequest(ErrorMessagesEnum.BadRequest);
				}
				await _gradesService.CreateGradeAsync(grade);
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
		public async Task<IActionResult> DeleteGradeAsync([FromRoute] Guid id)
		{
			try
			{
				_logger.LogInformation("DeleteGradeAsync started");
				bool result = await _gradesService.DeleteGradeAsync(id);
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
		public async Task<IActionResult> PutGrade([FromRoute] Guid id, [FromBody] CreateUpdateGrade grade)
		{
			try
			{
				_logger.LogInformation("Update started");
				if (grade == null)
				{
					return BadRequest(ErrorMessagesEnum.BadRequest);
				}
				CreateUpdateGrade updatedGrade = await _gradesService.UpdateGradeAsync(id, grade);
				if (updatedGrade == null)
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
		public async Task<IActionResult> PatchGrade([FromRoute] Guid id, [FromBody] PatchGrade grade)
		{
			try
			{
				_logger.LogInformation("Update started");
				if (grade == null)
				{
					return BadRequest(ErrorMessagesEnum.BadRequest);
				}
				PatchGrade updatedGrade = await _gradesService.UpdatePartiallyGradeAsync(id, grade);
				if (updatedGrade == null)
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
