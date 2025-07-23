using AuthTesting.Application.Abstractions.Services;
using AuthTesting.Contracts.Options;
using AuthTesting.Contracts.Requests;
using AuthTesting.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AuthTesting.Host.Endpoints;

public static class UserEndpointsExtensions
{
	public static IEndpointRouteBuilder UseUserEndpoints(this IEndpointRouteBuilder endpoints)
	{
		RouteGroupBuilder group = endpoints.MapGroup("/user").WithTags("User");
		group.MapGet("/{name}", GetUserAsync);
		group.MapPost("/register", RegisterUserAsync);
		group.MapPost("/login", LoginUserAsync);

		return endpoints;
	}

	private static async Task<IResult> GetUserAsync(
		[FromServices] IUserService service,
		string name)
	{
		User? user = await service.GetUserByNameAsync(name);

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

	/// <summary>
	///     puts jwt token in cookies so that it can be checked when it is challenge time
	/// </summary>
	/// <param name="service"></param>
	/// <param name="request"></param>
	/// <param name="context"></param>
	/// <returns></returns>
	private static async Task<IResult> LoginUserAsync(
		[FromServices] IUserService service,
		LoginRequest request,
		HttpContext context)
	{
		string jwt = await service.LoginAsync(request);

		if (jwt.IsNullOrEmpty())
		{
			return Results.Unauthorized();
		}

		context.Response.Cookies.Append(CustomCookieOptions.AuthenticationTokenName, jwt);

		return Results.Ok();
	}
}