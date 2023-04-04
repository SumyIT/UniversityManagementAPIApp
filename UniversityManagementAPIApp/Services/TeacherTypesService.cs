using UniversityManagementAPIApp.DTOs;
using UniversityManagementAPIApp.DTOs.CreateUpdateObjects;
using UniversityManagementAPIApp.DTOs.PatchObjects;
using UniversityManagementAPIApp.Repositories;

namespace UniversityManagementAPIApp.Services
{
	public class TeacherTypesService : ITeacherTypesService
	{
		private readonly ITeacherTypesRepository _repository;

		public TeacherTypesService(ITeacherTypesRepository repository)
		{
			_repository = repository;
		}

		public async Task<TeacherType> GetTeacherTypeByIdAsync(Guid id)
		{
			return await _repository.GetTeacherTypeByIdAsync(id);
		}

		public async Task<IEnumerable<TeacherType>> GetTeacherTypesAsync()
		{
			return await _repository.GetTeacherTypesAsync();
		}

		public async Task CreateTeacherTypeAsync(TeacherType newTeacherType)
		{
			await _repository.CreateTeacherTypeAsync(newTeacherType);
		}

		public async Task<bool> DeleteTeacherTypeAsync(Guid id)
		{
			return await _repository.DeleteTeacherTypeAsync(id);
		}

		public async Task<CreateUpdateTeacherType> UpdateTeacherTypeAsync(Guid id, CreateUpdateTeacherType teacherType)
		{
			return await _repository.UpdateTeacherTypeAsync(id, teacherType);
		}

		public async Task<PatchTeacherType> UpdatePartiallyTeacherTypeAsync(Guid id, PatchTeacherType teacherType)
		{
			return await _repository.UpdatePartiallyTeacherTypeAsync(id, teacherType);
		}
	}
}
