using AuthTesting.Domain.Enums;
using AuthTesting.Host.Extensions.Middleware;

namespace AuthTesting.Host.Endpoints;

public static class TestEndpointsForAuthorization
{
	public static IEndpointRouteBuilder UseTestEndpointsForAuthorization(this IEndpointRouteBuilder endpoints)
	{
		endpoints.MapGet("/get_cookie", GetCookie);

		endpoints.MapGet("/get", () => { return Results.Ok(); }).RequirePermissions(Permission.Read);

		endpoints.MapPost("/post", () => { return Results.Ok(); }).RequirePermissions(Permission.Create);

		return endpoints;
	}

	/// <summary>
	///     This is how cookie can be added. Use CustomCookieOptions to configure cookie
	/// </summary>
	/// <param name="context"></param>
	/// <returns></returns>
	private static Task GetCookie(
		HttpContext context)
	{
		context.Response.Cookies.Append("_SecretKey", "keey");

		return Task.CompletedTask;
	}
}