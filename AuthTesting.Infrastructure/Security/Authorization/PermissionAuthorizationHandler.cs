using System.Security.Claims;
using AuthTesting.Application.Abstractions.Services;
using AuthTesting.Contracts.ConstTypes;
using AuthTesting.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace AuthTesting.Infrastructure.Security.Authorization;

public class PermissionAuthorizationHandler
	: AuthorizationHandler<PermissionRequirement>
{
	private readonly IServiceScopeFactory _serviceScopeFactory;

	public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
	{
		_serviceScopeFactory = serviceScopeFactory;
	}

	protected override async Task HandleRequirementAsync(
		AuthorizationHandlerContext context,
		PermissionRequirement requirement)
	{
		IServiceScope scope = _serviceScopeFactory.CreateScope();
		IPermissionService permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();

		Claim userIdClaim = context.User.Claims
			.FirstOrDefault(c => c.Type == CustomClaimsTypes.UserId) ?? throw new Exception("No user claim found");

		string userId = userIdClaim.Value;

		if (userId.IsNullOrEmpty() || !Guid.TryParse(userId, out Guid userGuidId))
		{
			return;
		}

		Permission[] permissions = await permissionService.GetPermissionsByUserIdAsync(userGuidId);

		if (permissions.Intersect(requirement.Permissions).Any())
		{
			context.Succeed(requirement);
		}
	}
}