using AuthTesting.Application.Abstractions.Repositories;
using AuthTesting.Application.Abstractions.Services;
using AuthTesting.Domain.Entities;
using AuthTesting.Domain.Enums;

namespace AuthTesting.Application.Services;

public class PermissionService : IPermissionService
{
	private readonly IUserRepository _userRepository;

	public PermissionService(IUserRepository userRepository)
	{
		_userRepository = userRepository;
	}

	public async Task<Permission[]> GetPermissionsByUserIdAsync(Guid userId)
	{
		User? user = await _userRepository.GetUserByIdAsync(userId);

		return user?.Permissions ?? [];
	}
}