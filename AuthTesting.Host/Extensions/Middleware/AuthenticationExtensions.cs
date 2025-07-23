using System.Text;
using AuthTesting.Application.Abstractions.Services;
using AuthTesting.Application.Services;
using AuthTesting.Contracts.Options;
using AuthTesting.Domain.Enums;
using AuthTesting.Infrastructure.Security.Authentication;
using AuthTesting.Infrastructure.Security.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.IdentityModel.Tokens;

namespace AuthTesting.Host.Extensions.Middleware;

public static class ApiAuthenticationExtensions
{
	public static IServiceCollection AddApiAuthentication(
		this IServiceCollection services, 
		IConfiguration configuration)
	{
		var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();
		
		if (jwtOptions is null)
		{
			throw new NullReferenceException("Some options are missing.");
		}
		
		var secretKey = jwtOptions.SecretKey;

		services
			.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
				};

				options.Events = new JwtBearerEvents
				{
					OnMessageReceived = context =>
					{
						context.Token = context.Request.Cookies["token"];

						return Task.CompletedTask;
					}

				};
			});
			services.AddAuthorization();
			services.AddScoped<IPermissionService, PermissionService>();
			services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
			
			
			// .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
			// {
			// 	options.Cookie.Name = "igotcookie";
			// 	options.EventsType = typeof(CustomCookieAuthenticationEvents);
			// 	//options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
			// 	options.Cookie.MaxAge = TimeSpan.FromDays(30);
			// 	options.SlidingExpiration = true; //new per each new request
			// 	options.AccessDeniedPath = "/Forbidden";
			// 	options.Cookie.SameSite = SameSiteMode.Strict;
			// 	options.Cookie.HttpOnly = true;
			// 	options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
			// });
			
		return services;
	}

	public static IEndpointConventionBuilder RequirePermissions<TBuilder>(this TBuilder builder, params Permission[] permissions) 
		where TBuilder : IEndpointConventionBuilder
	{
		return builder.RequireAuthorization(policy =>
		{
			policy.AddRequirements(new PermissionRequirement(permissions));
		});
	}
}