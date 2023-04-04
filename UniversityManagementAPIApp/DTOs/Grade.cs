using System.ComponentModel.DataAnnotations;

namespace UniversityManagementAPIApp.DTOs
{
	public class Grade
	{
		[Key]
		public Guid IdGrade { get; set; }

		public Guid IdGradeType { get; set; }

		public Guid IdStudent { get; set; }

		public Guid IdSubject { get; set; }

		public int Value { get; set; }
	}
}
