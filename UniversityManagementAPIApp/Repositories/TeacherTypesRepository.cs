using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UniversityManagementAPIApp.DataContext;
using UniversityManagementAPIApp.DTOs;
using UniversityManagementAPIApp.DTOs.CreateUpdateObjects;
using UniversityManagementAPIApp.DTOs.PatchObjects;

namespace UniversityManagementAPIApp.Repositories
{
	public class TeacherTypesRepository : ITeacherTypesRepository
	{
		private readonly UniversityManagementDataContext _context;

		private readonly IMapper _mapper;

		public TeacherTypesRepository(UniversityManagementDataContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<TeacherType> GetTeacherTypeByIdAsync(Guid id)
		{
			return await _context.TeacherTypes.SingleOrDefaultAsync(a => a.IdTeacherType == id);
		}

		public async Task<IEnumerable<TeacherType>> GetTeacherTypesAsync()
		{
			return await _context.TeacherTypes.ToListAsync();
		}

		public async Task CreateTeacherTypeAsync(TeacherType teacherType)
		{
			teacherType.IdTeacherType = Guid.NewGuid();
			_context.TeacherTypes.Add(teacherType);
			await _context.SaveChangesAsync();
		}

		public async Task<bool> DeleteTeacherTypeAsync(Guid id)
		{
			TeacherType teacherType = await GetTeacherTypeByIdAsync(id);
			if (teacherType == null)
			{
				return false;
			}
			_context.TeacherTypes.Remove(teacherType);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<CreateUpdateTeacherType> UpdateTeacherTypeAsync(Guid id, CreateUpdateTeacherType teacherType)
		{
			if (!await ExistTeacherTypeAsync(id))
			{
				return null;
			}

			//announcementFromDb.EventDate = announcement.EventDate;
			//announcementFromDb.Text = announcement.Text;
			//announcementFromDb.Title = announcement.Title;
			//announcementFromDb.ValidFrom = announcement.ValidFrom;
			//announcementFromDb.ValidTo = announcement.ValidTo;
			//announcementFromDb.Tags = announcement.Tags;

			var updatedTeacherType = _mapper.Map<TeacherType>(teacherType);
			updatedTeacherType.IdTeacherType = id;
			_context.Update(updatedTeacherType);
			await _context.SaveChangesAsync();
			return teacherType;
		}

		public async Task<PatchTeacherType> UpdatePartiallyTeacherTypeAsync(Guid id, PatchTeacherType teacherType)
		{
			var teacherTypeFromDb = await GetTeacherTypeByIdAsync(id);
			if (teacherTypeFromDb == null)
			{
				return null;
			}
			if (!string.IsNullOrEmpty(teacherType.Name) && teacherType.Name != teacherTypeFromDb.Name)
			{
				teacherTypeFromDb.Name = teacherType.Name;
			}
			if (!string.IsNullOrEmpty(teacherType.Description) && teacherType.Description != teacherTypeFromDb.Description)
			{
				teacherTypeFromDb.Description = teacherType.Description;
			}

			_context.TeacherTypes.Update(teacherTypeFromDb);
			await _context.SaveChangesAsync();
			return teacherType;
		}
		private async Task<bool> ExistTeacherTypeAsync(Guid id)
		{
			return await _context.TeacherTypes.CountAsync(a => a.IdTeacherType == id) > 0;
		}
	}
}
