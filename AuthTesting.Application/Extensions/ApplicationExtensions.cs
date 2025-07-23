using Microsoft.Extensions.DependencyInjection;

namespace AuthTesting.Application.Extensions;

public static class ApplicationExtensions
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		services.AddServices();

		return services;
	}
}