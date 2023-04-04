using UniversityManagementAPIApp.DTOs;
using UniversityManagementAPIApp.Repositories;
using UniversityManagementAPIApp.Helpers;
using UniversityManagementAPIApp.DTOs.CreateUpdateObjects;
using UniversityManagementAPIApp.DTOs.PatchObjects;

namespace UniversityManagementAPIApp.Services
{
	public class GradesService : IGradesService
	{
		private readonly IGradesRepository _repository;

		public GradesService(IGradesRepository repository)
		{
			_repository = repository;
		}

		public async Task<Grade> GetGradeByIdAsync(Guid id)
		{
			return await _repository.GetGradeByIdAsync(id);
		}

		public async Task<IEnumerable<Grade>> GetGradesAsync()
		{
			return await _repository.GetGradesAsync();
		}

		public async Task CreateGradeAsync(Grade newGrade)
		{
			await _repository.CreateGradeAsync(newGrade);
		}

		public async Task<bool> DeleteGradeAsync(Guid id)
		{
			return await _repository.DeleteGradeAsync(id);
		}

		public async Task<CreateUpdateGrade> UpdateGradeAsync(Guid id, CreateUpdateGrade grade)
		{
			return await _repository.UpdateGradeAsync(id, grade);
		}

		public async Task<PatchGrade> UpdatePartiallyGradeAsync(Guid id, PatchGrade grade)
		{
			return await _repository.UpdatePartiallyGradeAsync(id, grade);
		}
	}
}
