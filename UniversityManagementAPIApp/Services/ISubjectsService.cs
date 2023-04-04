using UniversityManagementAPIApp.DTOs;
using UniversityManagementAPIApp.DTOs.CreateUpdateObjects;
using UniversityManagementAPIApp.DTOs.PatchObjects;

namespace UniversityManagementAPIApp.Services
{
	public interface ISubjectsService
	{
		public Task<Subject> GetSubjectByIdAsync(Guid id);

		public Task<IEnumerable<Subject>> GetSubjectsAsync();

		public Task CreateSubjectAsync(Subject newSubject);

		public Task<bool> DeleteSubjectAsync(Guid id);

		public Task<CreateUpdateSubject> UpdateSubjectAsync(Guid id, CreateUpdateSubject subject);

		public Task<PatchSubject> UpdatePartiallySubjectAsync(Guid id, PatchSubject subject);
	}
}
