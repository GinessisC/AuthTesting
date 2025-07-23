using AuthTesting.Application.Abstractions.Services;
using AuthTesting.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AuthTesting.Application.Extensions;

public static class ServicesExtensions
{
	public static IServiceCollection AddServices(this IServiceCollection services)
	{
		services.AddScoped<ICourseService, CourseService>();
		services.AddScoped<IUserService, UserService>();

		return services;
	}
}