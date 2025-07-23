using AuthTesting.Application.Abstractions.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;

namespace AuthTesting.Host.Extensions.Endpoints;

public static class CoursesEndpointsExtensions
{
	public static IEndpointRouteBuilder UseCoursesEndpoints(this IEndpointRouteBuilder endpoints)
	{
		var group = endpoints.MapGroup("/courses").WithTags("Courses")
			.RequireAuthorization();

		group.MapGet("/{id}", GetCourseByIdAsync);
		group.MapGet("/get_cookie", GetCookie);
		return endpoints;
	}
	/// <summary>
	/// This is how cookie can be added. Use CookieOptions to configure cookie
	/// </summary>
	/// <param name="context"></param>
	/// <returns></returns>
	private static Task GetCookie(HttpContext context)
	{
		context.Response.Cookies.Append("_SecretKey", "keey", new CookieOptions()
		{
			SameSite = SameSiteMode.None,
			Secure = true,
			HttpOnly = true,
		});

		return Task.CompletedTask;
	}

	private static async Task<IResult> GetCourseByIdAsync([FromServices] ICourseService courseService, int id)
	{
		var course = await courseService.GetCourseByIdAsync(id);

		if (course is null)
		{
			return Results.NotFound();
		}
		return Results.Ok(course);
	}
}