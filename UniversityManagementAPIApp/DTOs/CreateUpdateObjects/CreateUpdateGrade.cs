using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UniversityManagementAPIApp.DTOs.CreateUpdateObjects
{
	public class CreateUpdateGrade
	{
		[Key]
		[JsonIgnore]
		public Guid IdGrade { get; set; }

		[Required(ErrorMessage = "This field is required!")]
		public Guid IdGradeType { get; set; }

		[Required(ErrorMessage = "This field is required!")]
		public Guid IdStudent { get; set; }

		[Required(ErrorMessage = "This field is required!")]
		public Guid IdSubject { get; set; }

		[Required(ErrorMessage = "This field is required!")]
		public int Value { get; set; }
	}
}
