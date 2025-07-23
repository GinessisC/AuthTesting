namespace AuthTesting.Host.Endpoints;

public static class ApiEndpointsExtensions
{
	public static IEndpointRouteBuilder UseApiEndpoints(this IEndpointRouteBuilder endpoints)
	{
		endpoints
			.UseTestEndpointsForAuthorization()
			.UseCoursesEndpoints()
			.UseUserEndpoints();

		return endpoints;
	}
}