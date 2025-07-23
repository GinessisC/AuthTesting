using AuthTesting.Application.Abstractions.Security;
using AuthTesting.Application.Abstractions.Security.Authentication;
using AuthTesting.Infrastructure.Security.Authentication;
using AuthTesting.Infrastructure.Security.Hashing;
using Microsoft.Extensions.DependencyInjection;

namespace AuthTesting.Infrastructure.Extensions;

public static class SecurityExtensions
{
	public static IServiceCollection AddSecurity(this IServiceCollection services)
	{
		services.AddScoped<IJwtProvider, JwtProvider>();
		services.AddScoped<IPasswordHashing, PasswordHashing>();

		//services.AddSingleton<CustomCookieAuthenticationEvents>(); if cookie auth is used

		return services;
	}
}