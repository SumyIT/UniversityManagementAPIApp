using UniversityManagementAPIApp.DTOs;
using UniversityManagementAPIApp.DTOs.CreateUpdateObjects;
using UniversityManagementAPIApp.DTOs.PatchObjects;

namespace UniversityManagementAPIApp.Repositories
{
	public interface ITeacherTypesRepository
	{
		public Task<TeacherType> GetTeacherTypeByIdAsync(Guid id);

		public Task<IEnumerable<TeacherType>> GetTeacherTypesAsync();

		public Task CreateTeacherTypeAsync(TeacherType teacherType);

		public Task<bool> DeleteTeacherTypeAsync(Guid id);

		public Task<CreateUpdateTeacherType> UpdateTeacherTypeAsync(Guid id, CreateUpdateTeacherType teacherType);

		public Task<PatchTeacherType> UpdatePartiallyTeacherTypeAsync(Guid id, PatchTeacherType teacherType);
	}
}
