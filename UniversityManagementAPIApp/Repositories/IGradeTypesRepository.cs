using UniversityManagementAPIApp.DTOs.CreateUpdateObjects;
using UniversityManagementAPIApp.DTOs.PatchObjects;
using UniversityManagementAPIApp.DTOs;

namespace UniversityManagementAPIApp.Repositories
{
	public interface IGradeTypesRepository
	{
		public Task<GradeType> GetGradeTypeByIdAsync(Guid id);

		public Task<IEnumerable<GradeType>> GetGradeTypesAsync();

		public Task CreateGradeTypeAsync(GradeType gradeType);

		public Task<bool> DeleteGradeTypeAsync(Guid id);

		public Task<CreateUpdateGradeType> UpdateGradeTypeAsync(Guid id, CreateUpdateGradeType gradeType);

		public Task<PatchGradeType> UpdatePartiallyGradeTypeAsync(Guid id, PatchGradeType gradeType);
	}
}
