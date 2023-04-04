using AutoMapper;
using UniversityManagementAPIApp.DTOs;
using UniversityManagementAPIApp.DTOs.CreateUpdateObjects;

namespace UniversityManagementAPIApp
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Grade, CreateUpdateGrade>().ReverseMap();
			CreateMap<GradeType, CreateUpdateGradeType>().ReverseMap();
			CreateMap<Group, CreateUpdateGroup>().ReverseMap();
			CreateMap<Student, CreateUpdateStudent>().ReverseMap();
			CreateMap<Subject, CreateUpdateSubject>().ReverseMap();
			CreateMap<Teacher, CreateUpdateTeacher>().ReverseMap();
			CreateMap<TeacherType, CreateUpdateTeacherType>().ReverseMap();
		}
	}
}
