using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UniversityManagementAPIApp.DataContext;
using UniversityManagementAPIApp.DTOs;
using UniversityManagementAPIApp.DTOs.CreateUpdateObjects;
using UniversityManagementAPIApp.DTOs.PatchObjects;

namespace UniversityManagementAPIApp.Repositories
{
	public class TeachersRepository : ITeachersRepository
	{
		private readonly UniversityManagementDataContext _context;

		private readonly IMapper _mapper;

		public TeachersRepository(UniversityManagementDataContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<Teacher> GetTeacherByIdAsync(Guid id)
		{
			return await _context.Teachers.SingleOrDefaultAsync(a => a.IdTeacher == id);
		}

		public async Task<IEnumerable<Teacher>> GetTeachersAsync()
		{
			return await _context.Teachers.ToListAsync();
		}

		public async Task CreateTeacherAsync(Teacher teacher)
		{
			teacher.IdTeacher = Guid.NewGuid();
			_context.Teachers.Add(teacher);
			await _context.SaveChangesAsync();
		}

		public async Task<bool> DeleteTeacherAsync(Guid id)
		{
			Teacher teacher = await GetTeacherByIdAsync(id);
			if (teacher == null)
			{
				return false;
			}
			_context.Teachers.Remove(teacher);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<CreateUpdateTeacher> UpdateTeacherAsync(Guid id, CreateUpdateTeacher teacher)
		{
			if (!await ExistTeacherAsync(id))
			{
				return null;
			}

			//announcementFromDb.EventDate = announcement.EventDate;
			//announcementFromDb.Text = announcement.Text;
			//announcementFromDb.Title = announcement.Title;
			//announcementFromDb.ValidFrom = announcement.ValidFrom;
			//announcementFromDb.ValidTo = announcement.ValidTo;
			//announcementFromDb.Tags = announcement.Tags;

			var updatedTeacher = _mapper.Map<Teacher>(teacher);
			updatedTeacher.IdTeacher = id;
			_context.Update(updatedTeacher);
			await _context.SaveChangesAsync();
			return teacher;
		}

		public async Task<PatchTeacher> UpdatePartiallyTeacherAsync(Guid id, PatchTeacher teacher)
		{
			var teacherFromDb = await GetTeacherByIdAsync(id);
			if (teacherFromDb == null)
			{
				return null;
			}
			if (!string.IsNullOrEmpty(teacher.Name) && teacher.Name != teacherFromDb.Name)
			{
				teacherFromDb.Name = teacher.Name;
			}
			if (!string.IsNullOrEmpty(teacher.Description) && teacher.Description != teacherFromDb.Description)
			{
				teacherFromDb.Description = teacher.Description;
			}

			_context.Teachers.Update(teacherFromDb);
			await _context.SaveChangesAsync();
			return teacher;
		}
		private async Task<bool> ExistTeacherAsync(Guid id)
		{
			return await _context.Teachers.CountAsync(a => a.IdTeacher == id) > 0;
		}
	}
}
