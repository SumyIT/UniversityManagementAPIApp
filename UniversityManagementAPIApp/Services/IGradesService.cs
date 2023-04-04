using UniversityManagementAPIApp.DTOs;
using UniversityManagementAPIApp.DTOs.CreateUpdateObjects;
using UniversityManagementAPIApp.DTOs.PatchObjects;

namespace UniversityManagementAPIApp.Services
{
	public interface IGradesService
	{
		public Task<Grade> GetGradeByIdAsync(Guid id);

		public Task<IEnumerable<Grade>> GetGradesAsync();

		public Task CreateGradeAsync(Grade newGrade);

		public Task<bool> DeleteGradeAsync(Guid id);

		public Task<CreateUpdateGrade> UpdateGradeAsync(Guid id, CreateUpdateGrade grade);

		public Task<PatchGrade> UpdatePartiallyGradeAsync(Guid id, PatchGrade grade);
	}
}
