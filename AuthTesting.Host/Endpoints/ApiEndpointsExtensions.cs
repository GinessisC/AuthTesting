namespace AuthTesting.Host.Extensions.Endpoints;

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