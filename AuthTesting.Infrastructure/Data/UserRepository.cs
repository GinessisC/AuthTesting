using AuthTesting.Application.Abstractions.Repositories;
using AuthTesting.Domain.Entities;
using AuthTesting.Domain.Enums;

namespace AuthTesting.Infrastructure.Data;

public class UserRepository : IUserRepository
{
	private readonly IList<User> _users = new List<User>();

	public async Task RegisterAsync(string userName, string hashedPassword)
	{
		await Task.Delay(100);

		User user = new User
		{
			Id = Guid.NewGuid(),
			Name = userName,
			PasswordHash = hashedPassword,
			Permissions = [Permission.Read]
		};

		if (userName == "admin")
		{
			user.Permissions = [Permission.Create, Permission.Read, Permission.Update, Permission.Delete];
		}

		_users.Add(user);
	}

	public async Task<User?> GetUserAsync(string userName)
	{
		await Task.Delay(100);

		User? user = _users
			.FirstOrDefault(u => u.Name == userName);

		return user;
	}

	public async Task<User?> GetUserByIdAsync(Guid userId)
	{
		await Task.Delay(100);

		User? user = _users.FirstOrDefault(u => u.Id == userId);

		return user;
	}
}