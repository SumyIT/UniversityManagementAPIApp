﻿namespace UniversityManagementAPIApp.Models
{
	public class ModelValidationException : Exception
	{
		public ModelValidationException(string message) : base(message) { }
	}
}
