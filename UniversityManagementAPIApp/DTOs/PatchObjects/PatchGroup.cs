using System.ComponentModel.DataAnnotations;

namespace UniversityManagementAPIApp.DTOs.PatchObjects
{
	public class PatchGroup
	{
		[Key]
		public Guid? IdGroup { get; set; }

		public string? Name { get; set; }

		public string? Description { get; set; }
	}
}
