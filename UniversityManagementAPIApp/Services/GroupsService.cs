using UniversityManagementAPIApp.DTOs.CreateUpdateObjects;
using UniversityManagementAPIApp.DTOs.PatchObjects;
using UniversityManagementAPIApp.DTOs;
using UniversityManagementAPIApp.Repositories;

namespace UniversityManagementAPIApp.Services
{
	public class GroupsService : IGroupsService
	{
		private readonly IGroupsRepository _repository;

		public GroupsService(IGroupsRepository repository)
		{
			_repository = repository;
		}

		public async Task<Group> GetGroupByIdAsync(Guid id)
		{
			return await _repository.GetGroupByIdAsync(id);
		}

		public async Task<IEnumerable<Group>> GetGroupsAsync()
		{
			return await _repository.GetGroupsAsync();
		}

		public async Task CreateGroupAsync(Group newGroup)
		{
			await _repository.CreateGroupAsync(newGroup);
		}

		public async Task<bool> DeleteGroupAsync(Guid id)
		{
			return await _repository.DeleteGroupAsync(id);
		}

		public async Task<CreateUpdateGroup> UpdateGroupAsync(Guid id, CreateUpdateGroup group)
		{
			return await _repository.UpdateGroupAsync(id, group);
		}

		public async Task<PatchGroup> UpdatePartiallyGroupAsync(Guid id, PatchGroup group)
		{
			return await _repository.UpdatePartiallyGroupAsync(id, group);
		}
	}
}
