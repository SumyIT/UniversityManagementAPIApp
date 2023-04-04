using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UniversityManagementAPIApp.DTOs.CreateUpdateObjects;
using UniversityManagementAPIApp.DTOs.PatchObjects;
using UniversityManagementAPIApp.DTOs;
using UniversityManagementAPIApp.Helpers;
using UniversityManagementAPIApp.Models;
using UniversityManagementAPIApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace UniversityManagementAPIApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class GradeTypesController : ControllerBase
	{
		private readonly IGradeTypesService _gradeTypesService;

		private readonly ILogger<GradeTypesController> _logger;

		public GradeTypesController(IGradeTypesService gradeTypesService, ILogger<GradeTypesController> logger)
		{
			_gradeTypesService = gradeTypesService;
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllAsync()
		{
			try
			{
				_logger.LogInformation("GetGradeTypes started");
				var gradeTypes = await _gradeTypesService.GetGradeTypesAsync();
				if (gradeTypes == null || !gradeTypes.Any())
				{
					return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
				}

				return Ok(gradeTypes);
			}
			catch (Exception ex)
			{
				_logger.LogError($"GetAllGradeTypes error: {ex.Message}");
				return StatusCode((int)(HttpStatusCode.InternalServerError), ex.Message);
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetGradeTypeAsync([FromRoute] Guid id)
		{
			try
			{
				_logger.LogInformation("GetGradeTypes started");
				var gradeType = await _gradeTypesService.GetGradeTypeByIdAsync(id);
				if (gradeType == null)
				{
					return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
				}
				return Ok(gradeType);
			}
			catch (Exception ex)
			{
				_logger.LogError($"GetGradeTypeById error: {ex.Message}");
				return StatusCode((int)(HttpStatusCode.InternalServerError), ex.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> PostGradeTypeAsync([FromBody] GradeType gradeType)
		{
			try
			{
				_logger.LogInformation("CreateGradeTypeAsync started");
				if (gradeType == null)
				{
					return BadRequest(ErrorMessagesEnum.BadRequest);
				}
				await _gradeTypesService.CreateGradeTypeAsync(gradeType);
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
		public async Task<IActionResult> DeleteGradeTypeAsync([FromRoute] Guid id)
		{
			try
			{
				_logger.LogInformation("DeleteGradeTypeAsync started");
				bool result = await _gradeTypesService.DeleteGradeTypeAsync(id);
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
		public async Task<IActionResult> PutGradeType([FromRoute] Guid id, [FromBody] CreateUpdateGradeType gradeType)
		{
			try
			{
				_logger.LogInformation("Update started");
				if (gradeType == null)
				{
					return BadRequest(ErrorMessagesEnum.BadRequest);
				}
				CreateUpdateGradeType updatedGradeType = await _gradeTypesService.UpdateGradeTypeAsync(id, gradeType);
				if (updatedGradeType == null)
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
		public async Task<IActionResult> PatchGradeType([FromRoute] Guid id, [FromBody] PatchGradeType gradeType)
		{
			try
			{
				_logger.LogInformation("Update started");
				if (gradeType == null)
				{
					return BadRequest(ErrorMessagesEnum.BadRequest);
				}
				PatchGradeType updatedGradeType = await _gradeTypesService.UpdatePartiallyGradeTypeAsync(id, gradeType);
				if (updatedGradeType == null)
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
