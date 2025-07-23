using AuthTesting.Domain.Entities;

namespace AuthTesting.Application.Abstractions.Services;

public interface ICourseService
{
	Task<Course?> GetCourseByIdAsync(int id);
}