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

	public class SubjectsController : ControllerBase
	{
		private readonly ISubjectsService _subjectsService;

		private readonly ILogger<SubjectsController> _logger;

		public SubjectsController(ISubjectsService subjectsService, ILogger<SubjectsController> logger)
		{
			_subjectsService = subjectsService;
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllAsync()
		{
			try
			{
				_logger.LogInformation("GetSubjects started");
				var subjects = await _subjectsService.GetSubjectsAsync();
				if (subjects == null || !subjects.Any())
				{
					return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
				}

				return Ok(subjects);
			}
			catch (Exception ex)
			{
				_logger.LogError($"GetAllSubjects error: {ex.Message}");
				return StatusCode((int)(HttpStatusCode.InternalServerError), ex.Message);
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetSubjectAsync([FromRoute] Guid id)
		{
			try
			{
				_logger.LogInformation("GetSubjects started");
				var subject = await _subjectsService.GetSubjectByIdAsync(id);
				if (subject == null)
				{
					return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
				}
				return Ok(subject);
			}
			catch (Exception ex)
			{
				_logger.LogError($"GetSubjectById error: {ex.Message}");
				return StatusCode((int)(HttpStatusCode.InternalServerError), ex.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> PostSubjectAsync([FromBody] Subject subject)
		{
			try
			{
				_logger.LogInformation("CreateSubjectAsync started");
				if (subject == null)
				{
					return BadRequest(ErrorMessagesEnum.BadRequest);
				}
				await _subjectsService.CreateSubjectAsync(subject);
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
		public async Task<IActionResult> DeleteSubjectAsync([FromRoute] Guid id)
		{
			try
			{
				_logger.LogInformation("DeleteSubjectAsync started");
				bool result = await _subjectsService.DeleteSubjectAsync(id);
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
		public async Task<IActionResult> PutSubject([FromRoute] Guid id, [FromBody] CreateUpdateSubject subject)
		{
			try
			{
				_logger.LogInformation("Update started");
				if (subject == null)
				{
					return BadRequest(ErrorMessagesEnum.BadRequest);
				}
				CreateUpdateSubject updatedSubject = await _subjectsService.UpdateSubjectAsync(id, subject);
				if (updatedSubject == null)
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
		public async Task<IActionResult> PatchSubject([FromRoute] Guid id, [FromBody] PatchSubject subject)
		{
			try
			{
				_logger.LogInformation("Update started");
				if (subject == null)
				{
					return BadRequest(ErrorMessagesEnum.BadRequest);
				}
				PatchSubject updatedSubject = await _subjectsService.UpdatePartiallySubjectAsync(id, subject);
				if (updatedSubject == null)
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
