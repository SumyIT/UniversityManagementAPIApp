using UniversityManagementAPIApp.DTOs.CreateUpdateObjects;
using UniversityManagementAPIApp.DTOs.PatchObjects;
using UniversityManagementAPIApp.DTOs;

namespace UniversityManagementAPIApp.Repositories
{
	public interface IStudentsRepository
	{
		public Task<Student> GetStudentByIdAsync(Guid id);

		public Task<IEnumerable<Student>> GetStudentsAsync();

		public Task CreateStudentAsync(Student student);

		public Task<bool> DeleteStudentAsync(Guid id);

		public Task<CreateUpdateStudent> UpdateStudentAsync(Guid id, CreateUpdateStudent student);

		public Task<PatchStudent> UpdatePartiallyStudentAsync(Guid id, PatchStudent student);
	}
}
