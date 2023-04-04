using System.ComponentModel.DataAnnotations;

namespace UniversityManagementAPIApp.DTOs
{
	public class Subject
	{
		[Key]
		public Guid IdSubject { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }
	}
}
