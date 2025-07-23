using System.Security.Claims;
using AuthTesting.Application.Abstractions.Repositories;
using AuthTesting.Domain.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;

namespace AuthTesting.Infrastructure.Security.Authentication;

/// <summary>
///     This class is used when you want to add additional check when authentication via cookie is applied
/// </summary>
public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
{
	private readonly IUserRepository _userRepository;

	public CustomCookieAuthenticationEvents(IUserRepository userRepository)
	{
		_userRepository = userRepository;
	}

	public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
	{
		ClaimsPrincipal? principal = context.Principal;

		if (principal == null)
		{
			return;
		}

		string? name = principal.Claims
			.Where(c => c.Type == ClaimTypes.Name)
			.Select(c => c.Value)
			.FirstOrDefault();

		if (name.IsNullOrEmpty())
		{
			context.RejectPrincipal();

			return;
		}

		User? user = await _userRepository.GetUserAsync(name);

		if (user == null)
		{
			context.RejectPrincipal();
		}
	}
}