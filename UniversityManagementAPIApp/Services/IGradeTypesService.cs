using UniversityManagementAPIApp.DTOs.CreateUpdateObjects;
using UniversityManagementAPIApp.DTOs.PatchObjects;
using UniversityManagementAPIApp.DTOs;

namespace UniversityManagementAPIApp.Services
{
	public interface IGradeTypesService
	{
		public Task<GradeType> GetGradeTypeByIdAsync(Guid id);

		public Task<IEnumerable<GradeType>> GetGradeTypesAsync();

		public Task CreateGradeTypeAsync(GradeType newGradeType);

		public Task<bool> DeleteGradeTypeAsync(Guid id);

		public Task<CreateUpdateGradeType> UpdateGradeTypeAsync(Guid id, CreateUpdateGradeType gradeType);

		public Task<PatchGradeType> UpdatePartiallyGradeTypeAsync(Guid id, PatchGradeType gradeType);
	}
}
