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
	public class StudentsController : ControllerBase
	{
		private readonly IStudentsService _studentsService;

		private readonly ILogger<StudentsController> _logger;

		public StudentsController(IStudentsService studentsService, ILogger<StudentsController> logger)
		{
			_studentsService = studentsService;
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllAsync()
		{
			try
			{
				_logger.LogInformation("GetStudents started");
				var students = await _studentsService.GetStudentsAsync();
				if (students == null || !students.Any())
				{
					return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
				}

				return Ok(students);
			}
			catch (Exception ex)
			{
				_logger.LogError($"GetAllStudents error: {ex.Message}");
				return StatusCode((int)(HttpStatusCode.InternalServerError), ex.Message);
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetStudentAsync([FromRoute] Guid id)
		{
			try
			{
				_logger.LogInformation("GetStudents started");
				var student = await _studentsService.GetStudentByIdAsync(id);
				if (student == null)
				{
					return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
				}
				return Ok(student);
			}
			catch (Exception ex)
			{
				_logger.LogError($"GetStudentById error: {ex.Message}");
				return StatusCode((int)(HttpStatusCode.InternalServerError), ex.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> PostStudentAsync([FromBody] Student student)
		{
			try
			{
				_logger.LogInformation("CreateStudentAsync started");
				if (student == null)
				{
					return BadRequest(ErrorMessagesEnum.BadRequest);
				}
				await _studentsService.CreateStudentAsync(student);
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
		public async Task<IActionResult> DeleteStudentAsync([FromRoute] Guid id)
		{
			try
			{
				_logger.LogInformation("DeleteStudentAsync started");
				bool result = await _studentsService.DeleteStudentAsync(id);
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
		public async Task<IActionResult> PutStudent([FromRoute] Guid id, [FromBody] CreateUpdateStudent student)
		{
			try
			{
				_logger.LogInformation("Update started");
				if (student == null)
				{
					return BadRequest(ErrorMessagesEnum.BadRequest);
				}
				CreateUpdateStudent updatedStudent = await _studentsService.UpdateStudentAsync(id, student);
				if (updatedStudent == null)
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
		public async Task<IActionResult> PatchStudent([FromRoute] Guid id, [FromBody] PatchStudent student)
		{
			try
			{
				_logger.LogInformation("Update started");
				if (student == null)
				{
					return BadRequest(ErrorMessagesEnum.BadRequest);
				}
				PatchStudent updatedStudent = await _studentsService.UpdatePartiallyStudentAsync(id, student);
				if (updatedStudent == null)
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
