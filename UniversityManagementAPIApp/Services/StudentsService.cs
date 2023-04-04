using UniversityManagementAPIApp.DTOs;
using UniversityManagementAPIApp.DTOs.CreateUpdateObjects;
using UniversityManagementAPIApp.DTOs.PatchObjects;
using UniversityManagementAPIApp.Repositories;

namespace UniversityManagementAPIApp.Services
{
	public class StudentsService : IStudentsService
	{
		private readonly IStudentsRepository _repository;

		public StudentsService(IStudentsRepository repository)
		{
			_repository = repository;
		}

		public async Task<Student> GetStudentByIdAsync(Guid id)
		{
			return await _repository.GetStudentByIdAsync(id);
		}

		public async Task<IEnumerable<Student>> GetStudentsAsync()
		{
			return await _repository.GetStudentsAsync();
		}

		public async Task CreateStudentAsync(Student newStudent)
		{
			await _repository.CreateStudentAsync(newStudent);
		}

		public async Task<bool> DeleteStudentAsync(Guid id)
		{
			return await _repository.DeleteStudentAsync(id);
		}

		public async Task<CreateUpdateStudent> UpdateStudentAsync(Guid id, CreateUpdateStudent student)
		{
			return await _repository.UpdateStudentAsync(id, student);
		}

		public async Task<PatchStudent> UpdatePartiallyStudentAsync(Guid id, PatchStudent student)
		{
			return await _repository.UpdatePartiallyStudentAsync(id, student);
		}
	}
}
