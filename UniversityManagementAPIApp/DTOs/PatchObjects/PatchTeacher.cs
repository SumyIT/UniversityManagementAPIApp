using System.ComponentModel.DataAnnotations;

namespace UniversityManagementAPIApp.DTOs.PatchObjects
{
	public class PatchTeacher
	{
		[Key]
		public Guid? IdTeacher { get; set; }

		public Guid? IdTeacherType { get; set; }

		public Guid? IdSubject { get; set; }

		public string? Name { get; set; }

		public string? Description { get; set; }
	}
}
