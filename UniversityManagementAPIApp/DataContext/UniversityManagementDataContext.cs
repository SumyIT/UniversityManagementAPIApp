using Microsoft.EntityFrameworkCore;
using UniversityManagementAPIApp.DTOs;

namespace UniversityManagementAPIApp.DataContext
{
	public class UniversityManagementDataContext : DbContext
	{
		public UniversityManagementDataContext(DbContextOptions<UniversityManagementDataContext> options) : base(options) { }

		public DbSet<Grade> Grades { get; set; }

		public DbSet<GradeType> GradeTypes { get; set; }
		
		public DbSet<Group> Groups { get; set; }
		
		public DbSet<Student> Students { get; set; }
		
		public DbSet<Subject> Subjects { get; set; }

		public DbSet<Teacher> Teachers { get; set; }

		public DbSet<TeacherType> TeacherTypes { get; set; }
	}
}
