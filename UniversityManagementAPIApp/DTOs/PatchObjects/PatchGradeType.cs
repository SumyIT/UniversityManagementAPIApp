using System.ComponentModel.DataAnnotations;

namespace UniversityManagementAPIApp.DTOs.PatchObjects
{
	public class PatchGradeType
	{
		[Key]
		public Guid? IdGradeType { get; set; }

		public string? Name { get; set; }

		public string? Description { get; set; }
	}
}
