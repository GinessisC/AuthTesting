using AuthTesting.Application.Abstractions.Services;
using AuthTesting.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AuthTesting.Host.Endpoints;

public static class CoursesEndpointsExtensions
{
	public static IEndpointRouteBuilder UseCoursesEndpoints(this IEndpointRouteBuilder endpoints)
	{
		RouteGroupBuilder group = endpoints.MapGroup("/courses").WithTags("Courses")
			.RequireAuthorization();

		group.MapGet("/{id}", GetCourseByIdAsync);

		return endpoints;
	}

	private static async Task<IResult> GetCourseByIdAsync([FromServices] ICourseService courseService, int id)
	{
		Course? course = await courseService.GetCourseByIdAsync(id);

		if (course is null)
		{
			return Results.NotFound();
		}

		return Results.Ok(course);
	}
}