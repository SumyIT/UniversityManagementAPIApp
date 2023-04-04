using System.ComponentModel.DataAnnotations;

namespace UniversityManagementAPIApp.DTOs
{
	public class Group
	{
		[Key]
		public Guid IdGroup { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }
	}
}
