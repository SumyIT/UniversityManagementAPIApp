using System.ComponentModel.DataAnnotations;

namespace UniversityManagementAPIApp.DTOs
{
	public class GradeType
	{
		[Key]
		public Guid IdGradeType { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }
	}
}
