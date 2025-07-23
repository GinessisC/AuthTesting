using AuthTesting.Domain.Entities;

namespace AuthTesting.Application.Abstractions.Repositories;

public interface ICourseRepository
{
	Task<Course?> GetByIdAsync(int id);
}