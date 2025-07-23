using AuthTesting.Host.Endpoints;

namespace AuthTesting.Host.Extensions.Middleware;

public static class WebApplicationExtensions
{
	public static WebApplication UseApiMiddlewares(this WebApplication app, IConfiguration configuration)
	{
		app
			.UseSecurityMiddleware(configuration)
			.UseApiEndpoints();

		return app;
	}
}