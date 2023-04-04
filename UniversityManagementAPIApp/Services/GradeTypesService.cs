using UniversityManagementAPIApp.DTOs.CreateUpdateObjects;
using UniversityManagementAPIApp.DTOs.PatchObjects;
using UniversityManagementAPIApp.DTOs;
using UniversityManagementAPIApp.Repositories;

namespace UniversityManagementAPIApp.Services
{
	public class GradeTypesService : IGradeTypesService
	{
		private readonly IGradeTypesRepository _repository;

		public GradeTypesService(IGradeTypesRepository repository)
		{
			_repository = repository;
		}

		public async Task<GradeType> GetGradeTypeByIdAsync(Guid id)
		{
			return await _repository.GetGradeTypeByIdAsync(id);
		}

		public async Task<IEnumerable<GradeType>> GetGradeTypesAsync()
		{
			return await _repository.GetGradeTypesAsync();
		}

		public async Task CreateGradeTypeAsync(GradeType newGradeType)
		{
			await _repository.CreateGradeTypeAsync(newGradeType);
		}

		public async Task<bool> DeleteGradeTypeAsync(Guid id)
		{
			return await _repository.DeleteGradeTypeAsync(id);
		}

		public async Task<CreateUpdateGradeType> UpdateGradeTypeAsync(Guid id, CreateUpdateGradeType gradeType)
		{
			return await _repository.UpdateGradeTypeAsync(id, gradeType);
		}

		public async Task<PatchGradeType> UpdatePartiallyGradeTypeAsync(Guid id, PatchGradeType gradeType)
		{
			return await _repository.UpdatePartiallyGradeTypeAsync(id, gradeType);
		}
	}
}
