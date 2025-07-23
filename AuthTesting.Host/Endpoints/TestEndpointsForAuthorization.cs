using AuthTesting.Domain.Enums;
using AuthTesting.Host.Extensions.Middleware;
using AuthTesting.Infrastructure.Security.Authorization;

namespace AuthTesting.Host.Extensions.Endpoints;

public static class TestEndpointsForAuthorization
{
	public static IEndpointRouteBuilder UseTestEndpointsForAuthorization(this IEndpointRouteBuilder endpoints)
	{
		endpoints.MapGet("/get", () =>
		{
			return Results.Ok();
		}).RequirePermissions(Permission.Read);
		
		endpoints.MapPost("/post", () =>
		{
			return Results.Ok();
		}).RequirePermissions(Permission.Create);
		return endpoints;
	}
}