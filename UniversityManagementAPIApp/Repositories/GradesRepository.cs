using Microsoft.EntityFrameworkCore;
using UniversityManagementAPIApp.DataContext;
using UniversityManagementAPIApp.DTOs;
using UniversityManagementAPIApp.DTOs.CreateUpdateObjects;
using UniversityManagementAPIApp.DTOs.PatchObjects;
using AutoMapper;


namespace UniversityManagementAPIApp.Repositories
{
	public class GradesRepository : IGradesRepository
	{
		private readonly UniversityManagementDataContext _context;

		private readonly IMapper _mapper;

		public GradesRepository(UniversityManagementDataContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<Grade> GetGradeByIdAsync(Guid id)
		{
			return await _context.Grades.SingleOrDefaultAsync(a => a.IdGrade == id);
		}

		public async Task<IEnumerable<Grade>> GetGradesAsync()
		{
			return await _context.Grades.ToListAsync();
		}

		public async Task CreateGradeAsync(Grade grade)
		{
			grade.IdGrade = Guid.NewGuid();
			_context.Grades.Add(grade);
			await _context.SaveChangesAsync();
		}

		public async Task<bool> DeleteGradeAsync(Guid id)
		{
			Grade grade = await GetGradeByIdAsync(id);
			if (grade == null)
			{
				return false;
			}
			_context.Grades.Remove(grade);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<CreateUpdateGrade> UpdateGradeAsync(Guid id, CreateUpdateGrade grade)
		{
			if (!await ExistGradeAsync(id))
			{
				return null;
			}

			//announcementFromDb.EventDate = announcement.EventDate;
			//announcementFromDb.Text = announcement.Text;
			//announcementFromDb.Title = announcement.Title;
			//announcementFromDb.ValidFrom = announcement.ValidFrom;
			//announcementFromDb.ValidTo = announcement.ValidTo;
			//announcementFromDb.Tags = announcement.Tags;

			var updatedGrade = _mapper.Map<Grade>(grade);
			updatedGrade.IdGrade = id;
			_context.Update(updatedGrade);
			await _context.SaveChangesAsync();
			return grade;
		}

		public async Task<PatchGrade> UpdatePartiallyGradeAsync(Guid id, PatchGrade grade)
		{
			var gradeFromDb = await GetGradeByIdAsync(id);
			if (gradeFromDb == null)
			{
				return null;
			}
			if (grade.Value.HasValue && grade.Value != gradeFromDb.Value)
			{
				gradeFromDb.Value = (int)grade.Value;
			}

			_context.Grades.Update(gradeFromDb);
			await _context.SaveChangesAsync();
			return grade;
		}
		private async Task<bool> ExistGradeAsync(Guid id)
		{
			return await _context.Grades.CountAsync(a => a.IdGrade == id) > 0;
		}
	}
}
