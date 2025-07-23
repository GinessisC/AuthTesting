using Microsoft.AspNetCore.CookiePolicy;

namespace AuthTesting.Host.Middleware;

public static class SecurityMiddleware
{
	public static WebApplication UseSecurityMiddleware(this WebApplication app)
	{
		app
			.UseAuthentication()
			.UseAuthorization();
		
		app.UseCookiePolicy(new CookiePolicyOptions() //for additional security
		{
			MinimumSameSitePolicy = SameSiteMode.Strict,
			HttpOnly = HttpOnlyPolicy.Always,
			Secure = CookieSecurePolicy.Always
		});
		return app;
	}
}