using System.ComponentModel.DataAnnotations;

namespace UniversityManagementAPIApp.DTOs
{
	public class TeacherType
	{
		[Key]
		public Guid IdTeacherType { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }
	}
}
