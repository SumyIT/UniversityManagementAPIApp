using Microsoft.EntityFrameworkCore;
using UniversityManagementAPIApp.DataContext;
using UniversityManagementAPIApp.Repositories;
using UniversityManagementAPIApp.Services;
using UniversityManagementAPIApp.Services.UserService;
using log4net.Appender;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace UniversityManagementAPIApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI.
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(options =>
			{
				options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
				{
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					In = ParameterLocation.Header,
					Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n" +
					" Enter 'Bearer' [space] and then your token in the text input below." +
					"\r\n\r\nExample: \"Bearer 12345abcdef\"",
				});

				options.OperationFilter<SecurityRequirementsOperationFilter>();
			});
			builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = false,
						ValidateIssuerSigningKey = true,
						ValidIssuer = builder.Configuration["Authentication:Domain"],
						ValidAudience = builder.Configuration["Authentication:Audience"],
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Authentication:Secret"]))
					};
				});
			builder.Services.AddAuthorization();

			builder.Services.AddDbContext<UniversityManagementDataContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

			builder.Services.AddTransient<IGradesRepository, GradesRepository>();
			builder.Services.AddTransient<IGradesService, GradesService>();
			builder.Services.AddTransient<IGradeTypesRepository, GradeTypesRepository>();
			builder.Services.AddTransient<IGradeTypesService, GradeTypesService>();
			builder.Services.AddTransient<IGroupsRepository, GroupsRepository>();
			builder.Services.AddTransient<IGroupsService, GroupsService>();
			builder.Services.AddTransient<IStudentsRepository, StudentsRepository>();
			builder.Services.AddTransient<IStudentsService, StudentsService>();
			builder.Services.AddTransient<ISubjectsRepository, SubjectsRepository>();
			builder.Services.AddTransient<ISubjectsService, SubjectsService>();
			builder.Services.AddTransient<ITeachersRepository, TeachersRepository>();
			builder.Services.AddTransient<ITeachersService, TeachersService>();
			builder.Services.AddTransient<ITeacherTypesRepository, TeacherTypesRepository>();
			builder.Services.AddTransient<ITeacherTypesService, TeacherTypesService>();
			builder.Services.AddTransient<IUserService, UserService>();

			builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

			builder.Logging.AddLog4Net("log4net.config");

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();
			app.UseAuthentication();
			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}