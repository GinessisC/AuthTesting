using AuthTesting.Contracts.Requests;
using AuthTesting.Domain.Entities;

namespace AuthTesting.Application.Abstractions.Services;

public interface IUserService
{
	Task RegisterAsync(RegisterRequest request);
	Task<string> LoginAsync(LoginRequest request);
	Task<User?> GetUserByNameAsync(string userName);
}