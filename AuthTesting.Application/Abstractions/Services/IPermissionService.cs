using AuthTesting.Domain.Enums;

namespace AuthTesting.Application.Abstractions.Services;

public interface IPermissionService
{
	Task<Permission[]> GetPermissionsByUserIdAsync(Guid userId);
}