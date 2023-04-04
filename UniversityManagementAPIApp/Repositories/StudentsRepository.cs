using AutoMapper;
using UniversityManagementAPIApp.DataContext;
using UniversityManagementAPIApp.DTOs.CreateUpdateObjects;
using UniversityManagementAPIApp.DTOs.PatchObjects;
using UniversityManagementAPIApp.DTOs;
using Microsoft.EntityFrameworkCore;

namespace UniversityManagementAPIApp.Repositories
{
	public class StudentsRepository : IStudentsRepository
	{
		private readonly UniversityManagementDataContext _context;

		private readonly IMapper _mapper;

		public StudentsRepository(UniversityManagementDataContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<Student> GetStudentByIdAsync(Guid id)
		{
			return await _context.Students.SingleOrDefaultAsync(a => a.IdStudent == id);
		}

		public async Task<IEnumerable<Student>> GetStudentsAsync()
		{
			return await _context.Students.ToListAsync();
		}

		public async Task CreateStudentAsync(Student student)
		{
			student.IdStudent = Guid.NewGuid();
			_context.Students.Add(student);
			await _context.SaveChangesAsync();
		}

		public async Task<bool> DeleteStudentAsync(Guid id)
		{
			Student student = await GetStudentByIdAsync(id);
			if (student == null)
			{
				return false;
			}
			_context.Students.Remove(student);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<CreateUpdateStudent> UpdateStudentAsync(Guid id, CreateUpdateStudent student)
		{
			if (!await ExistStudentAsync(id))
			{
				return null;
			}

			//announcementFromDb.EventDate = announcement.EventDate;
			//announcementFromDb.Text = announcement.Text;
			//announcementFromDb.Title = announcement.Title;
			//announcementFromDb.ValidFrom = announcement.ValidFrom;
			//announcementFromDb.ValidTo = announcement.ValidTo;
			//announcementFromDb.Tags = announcement.Tags;

			var updatedStudent = _mapper.Map<Student>(student);
			updatedStudent.IdStudent = id;
			_context.Update(updatedStudent);
			await _context.SaveChangesAsync();
			return student;
		}

		public async Task<PatchStudent> UpdatePartiallyStudentAsync(Guid id, PatchStudent student)
		{
			var studentFromDb = await GetStudentByIdAsync(id);
			if (studentFromDb == null)
			{
				return null;
			}
			if (!string.IsNullOrEmpty(student.Name) && student.Name != studentFromDb.Name)
			{
				studentFromDb.Name = student.Name;
			}
			if (!string.IsNullOrEmpty(student.Description) && student.Description != studentFromDb.Description)
			{
				studentFromDb.Description = student.Description;
			}

			_context.Students.Update(studentFromDb);
			await _context.SaveChangesAsync();
			return student;
		}
		private async Task<bool> ExistStudentAsync(Guid id)
		{
			return await _context.Students.CountAsync(a => a.IdStudent == id) > 0;
		}
	}
}
