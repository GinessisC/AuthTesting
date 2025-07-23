using AuthTesting.Domain;

namespace AuthTesting.Application.Abstractions.Repositories;

public interface IRepository
{
	Task<Course?> GetByIdAsync(int id);
}