using AuthTesting.Host.Endpoints;

namespace AuthTesting.Host.Middleware;

public static class WebApplicationExtensions
{
	public static WebApplication UseApiMiddlewares(this WebApplication app)
	{
		app.UseApiEndpoints();

		app.UseSecurityMiddleware();
		
		return app;
	}
}