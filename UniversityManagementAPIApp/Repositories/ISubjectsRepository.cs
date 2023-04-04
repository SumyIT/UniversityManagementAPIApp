using UniversityManagementAPIApp.DTOs;
using UniversityManagementAPIApp.DTOs.CreateUpdateObjects;
using UniversityManagementAPIApp.DTOs.PatchObjects;

namespace UniversityManagementAPIApp.Repositories
{
	public interface ISubjectsRepository
	{
		public Task<Subject> GetSubjectByIdAsync(Guid id);

		public Task<IEnumerable<Subject>> GetSubjectsAsync();

		public Task CreateSubjectAsync(Subject subject);

		public Task<bool> DeleteSubjectAsync(Guid id);

		public Task<CreateUpdateSubject> UpdateSubjectAsync(Guid id, CreateUpdateSubject subject);

		public Task<PatchSubject> UpdatePartiallySubjectAsync(Guid id, PatchSubject subject);
	}
}
