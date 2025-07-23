using AuthTesting.Application.Abstractions.Repositories;
using AuthTesting.Domain;

namespace AuthTesting.Infrastructure.Data;

public class Repository : ICourseRepository
{
	public async Task<Course?> GetByIdAsync(int id)
	{
		throw new NotImplementedException();
	}
}