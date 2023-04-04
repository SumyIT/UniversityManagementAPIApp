using System.ComponentModel.DataAnnotations;

namespace UniversityManagementAPIApp.DTOs.PatchObjects
{
	public class PatchTeacherType
	{
		[Key]
		public Guid? IdTeacherType { get; set; }

		public string? Name { get; set; }

		public string? Description { get; set; }
	}
}
