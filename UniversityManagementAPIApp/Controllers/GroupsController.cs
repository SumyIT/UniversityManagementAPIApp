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

namespace UniversityManagementAPIApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class GroupsController : ControllerBase
	{
		private readonly IGroupsService _groupsService;

		private readonly ILogger<GroupsController> _logger;

		public GroupsController(IGroupsService groupsService, ILogger<GroupsController> logger)
		{
			_groupsService = groupsService;
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllAsync()
		{
			try
			{
				_logger.LogInformation("GetGroups started");
				var groups = await _groupsService.GetGroupsAsync();
				if (groups == null || !groups.Any())
				{
					return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
				}

				return Ok(groups);
			}
			catch (Exception ex)
			{
				_logger.LogError($"GetAllGroups error: {ex.Message}");
				return StatusCode((int)(HttpStatusCode.InternalServerError), ex.Message);
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetGroupAsync([FromRoute] Guid id)
		{
			try
			{
				_logger.LogInformation("GetGroups started");
				var group = await _groupsService.GetGroupByIdAsync(id);
				if (group == null)
				{
					return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
				}
				return Ok(group);
			}
			catch (Exception ex)
			{
				_logger.LogError($"GetGroupById error: {ex.Message}");
				return StatusCode((int)(HttpStatusCode.InternalServerError), ex.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> PostGroupAsync([FromBody] Group group)
		{
			try
			{
				_logger.LogInformation("CreateGroupAsync started");
				if (group == null)
				{
					return BadRequest(ErrorMessagesEnum.BadRequest);
				}
				await _groupsService.CreateGroupAsync(group);
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
		public async Task<IActionResult> DeleteGroupAsync([FromRoute] Guid id)
		{
			try
			{
				_logger.LogInformation("DeleteGroupAsync started");
				bool result = await _groupsService.DeleteGroupAsync(id);
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
		public async Task<IActionResult> PutGroup([FromRoute] Guid id, [FromBody] CreateUpdateGroup group)
		{
			try
			{
				_logger.LogInformation("Update started");
				if (group == null)
				{
					return BadRequest(ErrorMessagesEnum.BadRequest);
				}
				CreateUpdateGroup updatedGroup = await _groupsService.UpdateGroupAsync(id, group);
				if (updatedGroup == null)
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
		public async Task<IActionResult> PatchGroup([FromRoute] Guid id, [FromBody] PatchGroup group)
		{
			try
			{
				_logger.LogInformation("Update started");
				if (group == null)
				{
					return BadRequest(ErrorMessagesEnum.BadRequest);
				}
				PatchGroup updatedGroup = await _groupsService.UpdatePartiallyGroupAsync(id, group);
				if (updatedGroup == null)
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
