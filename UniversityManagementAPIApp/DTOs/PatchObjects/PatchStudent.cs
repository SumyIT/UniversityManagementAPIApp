using System.ComponentModel.DataAnnotations;

namespace UniversityManagementAPIApp.DTOs.PatchObjects
{
	public class PatchStudent
	{
		[Key]
		public Guid? IdStudent { get; set; }

		public Guid? IdGrade { get; set; }

		public string? Name { get; set; }

		public string? Description { get; set; }
	}
}
