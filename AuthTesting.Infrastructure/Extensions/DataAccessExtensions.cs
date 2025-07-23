using AuthTesting.Application.Abstractions.Repositories;
using AuthTesting.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace AuthTesting.Infrastructure.Extensions;

public static class DataAccessExtensions
{
	public static IServiceCollection AddDataAccess(this IServiceCollection services)
	{
		services.AddScoped<ICourseRepository, CourseRepository>();
		services.AddSingleton<IUserRepository, UserRepository>();

		return services;
	}
}