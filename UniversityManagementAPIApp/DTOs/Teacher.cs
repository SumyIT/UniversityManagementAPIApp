using System.ComponentModel.DataAnnotations;

namespace UniversityManagementAPIApp.DTOs
{
	public class Teacher
	{
		[Key]
		public Guid IdTeacher { get; set; }

		public Guid IdTeacherType { get; set; }

		public Guid IdSubject { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }
	}
}
