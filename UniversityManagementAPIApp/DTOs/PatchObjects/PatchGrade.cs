using System.ComponentModel.DataAnnotations;

namespace UniversityManagementAPIApp.DTOs.PatchObjects
{
	public class PatchGrade
	{
		[Key]
		public Guid? IdGrade { get; set; }

		public Guid? IdGradeType { get; set; }

		public Guid? IdStudent { get; set; }

		public Guid? IdSubject { get; set; }

		public int? Value { get; set; }
	}
}
