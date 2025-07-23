using AuthTesting.Contracts.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthTesting.Infrastructure.Extensions;

public static class InfrastructureExtensions
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		services
			.AddDataAccess()
			.AddSecurity()
			.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));

		return services;
	}
}