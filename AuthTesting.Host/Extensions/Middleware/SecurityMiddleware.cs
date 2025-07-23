namespace AuthTesting.Host.Extensions.Middleware;

public static class SecurityMiddleware
{
	public static WebApplication UseSecurityMiddleware(this WebApplication app, IConfiguration configuration)
	{
		CookiePolicyOptions? cookiePolicyOptions =
			configuration.GetSection(nameof(CookiePolicyOptions)).Get<CookiePolicyOptions>();

		if (cookiePolicyOptions is null)
		{
			throw new NullReferenceException("Some options are missing.");
		}

		app
			.UseHttpsRedirection()
			.UseCookiePolicy(
				cookiePolicyOptions) //checks if CookiePolicyOptions exist in services. Uses injected policy
			.UseAuthentication()
			.UseAuthorization();

		return app;
	}
}