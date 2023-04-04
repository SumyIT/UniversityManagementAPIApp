﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UniversityManagementAPIApp.DTOs.CreateUpdateObjects
{
	public class CreateUpdateTeacher
	{
		[Key]
		[JsonIgnore]
		public Guid IdTeacher { get; set; }

		[Required(ErrorMessage = "This field is required!")]
		public Guid IdTeacherType { get; set; }

		[Required(ErrorMessage = "This field is required!")]
		public Guid IdSubject { get; set; }

		[Required(ErrorMessage = "This field is required!")]
		[StringLength(250, ErrorMessage = "The Name field can contain a maximum of 250 characters.")]
		public string Name { get; set; }

		[Required(ErrorMessage = "This field is required!")]
		[StringLength(1000, ErrorMessage = "The Description field can contain a maximum of 1000 characters.")]
		public string Description { get; set; }
	}
}
