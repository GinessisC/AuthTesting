using AuthTesting.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace AuthTesting.Infrastructure.Security.Authorization;

public class PermissionRequirement : IAuthorizationRequirement
{
	public Permission[] Permissions { get; }

	public PermissionRequirement(Permission[] permissions)
	{
		Permissions = permissions;
	}
}