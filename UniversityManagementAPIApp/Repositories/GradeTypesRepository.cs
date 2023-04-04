using Microsoft.EntityFrameworkCore;
using AutoMapper;
using UniversityManagementAPIApp.DataContext;
using UniversityManagementAPIApp.DTOs.CreateUpdateObjects;
using UniversityManagementAPIApp.DTOs.PatchObjects;
using UniversityManagementAPIApp.DTOs;

namespace UniversityManagementAPIApp.Repositories
{
	public class GradeTypesRepository : IGradeTypesRepository
	{
		private readonly UniversityManagementDataContext _context;

		private readonly IMapper _mapper;

		public GradeTypesRepository(UniversityManagementDataContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<GradeType> GetGradeTypeByIdAsync(Guid id)
		{
			return await _context.GradeTypes.SingleOrDefaultAsync(a => a.IdGradeType == id);
		}

		public async Task<IEnumerable<GradeType>> GetGradeTypesAsync()
		{
			return await _context.GradeTypes.ToListAsync();
		}

		public async Task CreateGradeTypeAsync(GradeType gradeType)
		{
			gradeType.IdGradeType = Guid.NewGuid();
			_context.GradeTypes.Add(gradeType);
			await _context.SaveChangesAsync();
		}

		public async Task<bool> DeleteGradeTypeAsync(Guid id)
		{
			GradeType gradeType = await GetGradeTypeByIdAsync(id);
			if (gradeType == null)
			{
				return false;
			}
			_context.GradeTypes.Remove(gradeType);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<CreateUpdateGradeType> UpdateGradeTypeAsync(Guid id, CreateUpdateGradeType gradeType)
		{
			if (!await ExistGradeTypeAsync(id))
			{
				return null;
			}

			//announcementFromDb.EventDate = announcement.EventDate;
			//announcementFromDb.Text = announcement.Text;
			//announcementFromDb.Title = announcement.Title;
			//announcementFromDb.ValidFrom = announcement.ValidFrom;
			//announcementFromDb.ValidTo = announcement.ValidTo;
			//announcementFromDb.Tags = announcement.Tags;

			var updatedGradeType = _mapper.Map<GradeType>(gradeType);
			updatedGradeType.IdGradeType = id;
			_context.Update(updatedGradeType);
			await _context.SaveChangesAsync();
			return gradeType;
		}

		public async Task<PatchGradeType> UpdatePartiallyGradeTypeAsync(Guid id, PatchGradeType gradeType)
		{
			var gradeTypeFromDb = await GetGradeTypeByIdAsync(id);
			if (gradeTypeFromDb == null)
			{
				return null;
			}
			if (!string.IsNullOrEmpty(gradeType.Name) && gradeType.Name != gradeTypeFromDb.Name)
			{
				gradeTypeFromDb.Name = gradeType.Name;
			}
			if (!string.IsNullOrEmpty(gradeType.Description) && gradeType.Description != gradeTypeFromDb.Description)
			{
				gradeTypeFromDb.Description = gradeType.Description;
			}

			_context.GradeTypes.Update(gradeTypeFromDb);
			await _context.SaveChangesAsync();
			return gradeType;
		}
		private async Task<bool> ExistGradeTypeAsync(Guid id)
		{
			return await _context.GradeTypes.CountAsync(a => a.IdGradeType == id) > 0;
		}
	}
}
