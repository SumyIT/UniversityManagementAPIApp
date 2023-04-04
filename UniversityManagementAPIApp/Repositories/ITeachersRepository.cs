using UniversityManagementAPIApp.DTOs;
using UniversityManagementAPIApp.DTOs.CreateUpdateObjects;
using UniversityManagementAPIApp.DTOs.PatchObjects;

namespace UniversityManagementAPIApp.Repositories
{
	public interface ITeachersRepository
	{
		public Task<Teacher> GetTeacherByIdAsync(Guid id);

		public Task<IEnumerable<Teacher>> GetTeachersAsync();

		public Task CreateTeacherAsync(Teacher teacher);

		public Task<bool> DeleteTeacherAsync(Guid id);

		public Task<CreateUpdateTeacher> UpdateTeacherAsync(Guid id, CreateUpdateTeacher teacher);

		public Task<PatchTeacher> UpdatePartiallyTeacherAsync(Guid id, PatchTeacher teacher);
	}
}
