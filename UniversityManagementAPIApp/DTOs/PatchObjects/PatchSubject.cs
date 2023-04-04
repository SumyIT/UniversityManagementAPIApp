﻿using System.ComponentModel.DataAnnotations;

namespace UniversityManagementAPIApp.DTOs.PatchObjects
{
	public class PatchSubject
	{
		[Key]
		public Guid? IdSubject { get; set; }

		public string? Name { get; set; }

		public string? Description { get; set; }
	}
}
