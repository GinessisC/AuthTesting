using AuthTesting.Application.Abstractions.Repositories;
using AuthTesting.Application.Abstractions.Services;
using AuthTesting.Domain.Entities;

namespace AuthTesting.Application.Services;

public class CourseService(
	ICourseRepository repo)
	: ICourseService
{
	public async Task<Course?> GetCourseByIdAsync(int id)
	{
		Course? course = await repo.GetByIdAsync(id);

		return course;
	}
}