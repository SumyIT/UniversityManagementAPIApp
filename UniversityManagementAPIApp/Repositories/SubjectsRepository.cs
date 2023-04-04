using AutoMapper;
using UniversityManagementAPIApp.DataContext;
using UniversityManagementAPIApp.DTOs;
using UniversityManagementAPIApp.DTOs.CreateUpdateObjects;
using UniversityManagementAPIApp.DTOs.PatchObjects;
using Microsoft.EntityFrameworkCore;

namespace UniversityManagementAPIApp.Repositories
{
	public class SubjectsRepository : ISubjectsRepository
	{
		private readonly UniversityManagementDataContext _context;

		private readonly IMapper _mapper;

		public SubjectsRepository(UniversityManagementDataContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<Subject> GetSubjectByIdAsync(Guid id)
		{
			return await _context.Subjects.SingleOrDefaultAsync(a => a.IdSubject == id);
		}

		public async Task<IEnumerable<Subject>> GetSubjectsAsync()
		{
			return await _context.Subjects.ToListAsync();
		}

		public async Task CreateSubjectAsync(Subject subject)
		{
			subject.IdSubject = Guid.NewGuid();
			_context.Subjects.Add(subject);
			await _context.SaveChangesAsync();
		}

		public async Task<bool> DeleteSubjectAsync(Guid id)
		{
			Subject subject = await GetSubjectByIdAsync(id);
			if (subject == null)
			{
				return false;
			}
			_context.Subjects.Remove(subject);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<CreateUpdateSubject> UpdateSubjectAsync(Guid id, CreateUpdateSubject subject)
		{
			if (!await ExistSubjectAsync(id))
			{
				return null;
			}

			//announcementFromDb.EventDate = announcement.EventDate;
			//announcementFromDb.Text = announcement.Text;
			//announcementFromDb.Title = announcement.Title;
			//announcementFromDb.ValidFrom = announcement.ValidFrom;
			//announcementFromDb.ValidTo = announcement.ValidTo;
			//announcementFromDb.Tags = announcement.Tags;

			var updatedSubject = _mapper.Map<Subject>(subject);
			updatedSubject.IdSubject = id;
			_context.Update(updatedSubject);
			await _context.SaveChangesAsync();
			return subject;
		}

		public async Task<PatchSubject> UpdatePartiallySubjectAsync(Guid id, PatchSubject subject)
		{
			var subjectFromDb = await GetSubjectByIdAsync(id);
			if (subjectFromDb == null)
			{
				return null;
			}
			if (!string.IsNullOrEmpty(subject.Name) && subject.Name != subjectFromDb.Name)
			{
				subjectFromDb.Name = subject.Name;
			}
			if (!string.IsNullOrEmpty(subject.Description) && subject.Description != subjectFromDb.Description)
			{
				subjectFromDb.Description = subject.Description;
			}

			_context.Subjects.Update(subjectFromDb);
			await _context.SaveChangesAsync();
			return subject;
		}
		private async Task<bool> ExistSubjectAsync(Guid id)
		{
			return await _context.Subjects.CountAsync(a => a.IdSubject == id) > 0;
		}
	}
}
