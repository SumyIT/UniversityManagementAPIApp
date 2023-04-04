using UniversityManagementAPIApp.DTOs.CreateUpdateObjects;
using UniversityManagementAPIApp.DTOs.PatchObjects;
using UniversityManagementAPIApp.DTOs;

namespace UniversityManagementAPIApp.Services
{
	public interface IGroupsService
	{
		public Task<Group> GetGroupByIdAsync(Guid id);

		public Task<IEnumerable<Group>> GetGroupsAsync();

		public Task CreateGroupAsync(Group newGroup);

		public Task<bool> DeleteGroupAsync(Guid id);

		public Task<CreateUpdateGroup> UpdateGroupAsync(Guid id, CreateUpdateGroup group);

		public Task<PatchGroup> UpdatePartiallyGroupAsync(Guid id, PatchGroup group);
	}
}
