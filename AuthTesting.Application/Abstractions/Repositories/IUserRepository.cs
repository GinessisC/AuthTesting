using AuthTesting.Domain.Entities;

namespace AuthTesting.Application.Abstractions.Repositories;

public interface IUserRepository
{

	Task RegisterAsync(string userName, string hashedPassword);
	Task<User?> GetUserAsync(string userName);
	Task<User?> GetUserByIdAsync(Guid userId); //mb use delegate
}