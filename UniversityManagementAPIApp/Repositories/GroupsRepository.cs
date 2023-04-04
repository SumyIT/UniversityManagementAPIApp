using AutoMapper;
using UniversityManagementAPIApp.DataContext;
using UniversityManagementAPIApp.DTOs.CreateUpdateObjects;
using UniversityManagementAPIApp.DTOs.PatchObjects;
using UniversityManagementAPIApp.DTOs;
using Microsoft.EntityFrameworkCore;

namespace UniversityManagementAPIApp.Repositories
{
	public class GroupsRepository : IGroupsRepository
	{
		private readonly UniversityManagementDataContext _context;

		private readonly IMapper _mapper;

		public GroupsRepository(UniversityManagementDataContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<Group> GetGroupByIdAsync(Guid id)
		{
			return await _context.Groups.SingleOrDefaultAsync(a => a.IdGroup == id);
		}

		public async Task<IEnumerable<Group>> GetGroupsAsync()
		{
			return await _context.Groups.ToListAsync();
		}

		public async Task CreateGroupAsync(Group group)
		{
			group.IdGroup = Guid.NewGuid();
			_context.Groups.Add(group);
			await _context.SaveChangesAsync();
		}

		public async Task<bool> DeleteGroupAsync(Guid id)
		{
			Group group = await GetGroupByIdAsync(id);
			if (group == null)
			{
				return false;
			}
			_context.Groups.Remove(group);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<CreateUpdateGroup> UpdateGroupAsync(Guid id, CreateUpdateGroup group)
		{
			if (!await ExistGroupAsync(id))
			{
				return null;
			}

			//announcementFromDb.EventDate = announcement.EventDate;
			//announcementFromDb.Text = announcement.Text;
			//announcementFromDb.Title = announcement.Title;
			//announcementFromDb.ValidFrom = announcement.ValidFrom;
			//announcementFromDb.ValidTo = announcement.ValidTo;
			//announcementFromDb.Tags = announcement.Tags;

			var updatedGroup = _mapper.Map<Group>(group);
			updatedGroup.IdGroup = id;
			_context.Update(updatedGroup);
			await _context.SaveChangesAsync();
			return group;
		}

		public async Task<PatchGroup> UpdatePartiallyGroupAsync(Guid id, PatchGroup group)
		{
			var groupFromDb = await GetGroupByIdAsync(id);
			if (groupFromDb == null)
			{
				return null;
			}
			if (!string.IsNullOrEmpty(group.Name) && group.Name != groupFromDb.Name)
			{
				groupFromDb.Name = group.Name;
			}
			if (!string.IsNullOrEmpty(group.Description) && group.Description != groupFromDb.Description)
			{
				groupFromDb.Description = group.Description;
			}

			_context.Groups.Update(groupFromDb);
			await _context.SaveChangesAsync();
			return group;
		}
		private async Task<bool> ExistGroupAsync(Guid id)
		{
			return await _context.Groups.CountAsync(a => a.IdGroup == id) > 0;
		}
	}
}
