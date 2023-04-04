using UniversityManagementAPIApp.DTOs;
using UniversityManagementAPIApp.DTOs.CreateUpdateObjects;
using UniversityManagementAPIApp.DTOs.PatchObjects;
using UniversityManagementAPIApp.Repositories;

namespace UniversityManagementAPIApp.Services
{
	public class TeachersService : ITeachersService
	{
		private readonly ITeachersRepository _repository;

		public TeachersService(ITeachersRepository repository)
		{
			_repository = repository;
		}

		public async Task<Teacher> GetTeacherByIdAsync(Guid id)
		{
			return await _repository.GetTeacherByIdAsync(id);
		}

		public async Task<IEnumerable<Teacher>> GetTeachersAsync()
		{
			return await _repository.GetTeachersAsync();
		}

		public async Task CreateTeacherAsync(Teacher newTeacher)
		{
			await _repository.CreateTeacherAsync(newTeacher);
		}

		public async Task<bool> DeleteTeacherAsync(Guid id)
		{
			return await _repository.DeleteTeacherAsync(id);
		}

		public async Task<CreateUpdateTeacher> UpdateTeacherAsync(Guid id, CreateUpdateTeacher teacher)
		{
			return await _repository.UpdateTeacherAsync(id, teacher);
		}

		public async Task<PatchTeacher> UpdatePartiallyTeacherAsync(Guid id, PatchTeacher teacher)
		{
			return await _repository.UpdatePartiallyTeacherAsync(id, teacher);
		}
	}
}
