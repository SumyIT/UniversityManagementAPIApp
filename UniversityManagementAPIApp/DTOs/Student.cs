using System.ComponentModel.DataAnnotations;

namespace UniversityManagementAPIApp.DTOs
{
	public class Student
	{
		[Key]
		public Guid IdStudent { get; set; }

		public Guid IdGroup { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }
	}
}
