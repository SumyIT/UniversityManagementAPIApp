using UniversityManagementAPIApp.DTOs;
using UniversityManagementAPIApp.DTOs.CreateUpdateObjects;
using UniversityManagementAPIApp.DTOs.PatchObjects;

namespace UniversityManagementAPIApp.Services
{
	public interface ITeacherTypesService
	{
		public Task<TeacherType> GetTeacherTypeByIdAsync(Guid id);

		public Task<IEnumerable<TeacherType>> GetTeacherTypesAsync();

		public Task CreateTeacherTypeAsync(TeacherType newTeacherType);

		public Task<bool> DeleteTeacherTypeAsync(Guid id);

		public Task<CreateUpdateTeacherType> UpdateTeacherTypeAsync(Guid id, CreateUpdateTeacherType teacherType);

		public Task<PatchTeacherType> UpdatePartiallyTeacherTypeAsync(Guid id, PatchTeacherType teacherType);
	}
}
