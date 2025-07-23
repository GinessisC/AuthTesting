using System.Security.Claims;
using AuthTesting.Application.Abstractions.Services;
using AuthTesting.Contracts.Requests;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AuthTesting.Host.Extensions.Endpoints;

public static class UserEndpointsExtensions
{
	public static IEndpointRouteBuilder UseUserEndpoints(this IEndpointRouteBuilder endpoints)
	{
		var group = endpoints.MapGroup("/user").WithTags("User");
		group.MapGet("/{name}", GetUserAsync);
		group.MapPost("/register", RegisterUserAsync); //TODO why post
		group.MapPost("/login", LoginUserAsync);
		
		return endpoints;
	}

	private static async Task<IResult> GetUserAsync([FromServices] IUserService service, string name)
	{
		var user = await service.GetByNameAsync(name);

		if (user == null)
		{
			return Results.NotFound(user);
		}
		return Results.Ok(user);
	}

	private static async Task<IResult> RegisterUserAsync([FromServices] IUserService service, RegisterRequest request)
	{
		await service.RegisterAsync(request);

		return Results.Ok();
	}
	private static async Task<IResult> LoginUserAsync(
		[FromServices] IUserService service,
		LoginRequest request,
		HttpContext context)
	{
		var jwt = await service.LoginAsync(request);

		if (jwt.IsNullOrEmpty())
		{
			return Results.Unauthorized();
		}
		context.Response.Cookies.Append("token", jwt);
		return Results.Ok();
	}
}