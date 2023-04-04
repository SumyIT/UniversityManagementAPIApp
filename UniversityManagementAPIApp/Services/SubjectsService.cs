using UniversityManagementAPIApp.DTOs;
using UniversityManagementAPIApp.DTOs.CreateUpdateObjects;
using UniversityManagementAPIApp.DTOs.PatchObjects;
using UniversityManagementAPIApp.Repositories;

namespace UniversityManagementAPIApp.Services
{
	public class SubjectsService : ISubjectsService
	{
		private readonly ISubjectsRepository _repository;

		public SubjectsService(ISubjectsRepository repository)
		{
			_repository = repository;
		}

		public async Task<Subject> GetSubjectByIdAsync(Guid id)
		{
			return await _repository.GetSubjectByIdAsync(id);
		}

		public async Task<IEnumerable<Subject>> GetSubjectsAsync()
		{
			return await _repository.GetSubjectsAsync();
		}

		public async Task CreateSubjectAsync(Subject newSubject)
		{
			await _repository.CreateSubjectAsync(newSubject);
		}

		public async Task<bool> DeleteSubjectAsync(Guid id)
		{
			return await _repository.DeleteSubjectAsync(id);
		}

		public async Task<CreateUpdateSubject> UpdateSubjectAsync(Guid id, CreateUpdateSubject subject)
		{
			return await _repository.UpdateSubjectAsync(id, subject);
		}

		public async Task<PatchSubject> UpdatePartiallySubjectAsync(Guid id, PatchSubject subject)
		{
			return await _repository.UpdatePartiallySubjectAsync(id, subject);
		}
	}
}
