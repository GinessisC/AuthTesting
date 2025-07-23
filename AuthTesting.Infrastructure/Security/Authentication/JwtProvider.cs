using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthTesting.Application.Abstractions.Security.Authentication;
using AuthTesting.Contracts.ConstTypes;
using AuthTesting.Contracts.Options;
using AuthTesting.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthTesting.Infrastructure.Security.Authentication;

public class JwtProvider : IJwtProvider
{
	private readonly JwtOptions _jwtOptions;

	public JwtProvider(IOptions<JwtOptions> options)
	{
		_jwtOptions = options.Value;
	}

	public string GetJwtToken(User user)
	{
		Claim[] claims =
		{
			new Claim(CustomClaimsTypes.UserId, user.Id.ToString())
		};

		SigningCredentials signingCredentials = new SigningCredentials(
			new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
			SecurityAlgorithms.HmacSha256);

		JwtSecurityToken token = new JwtSecurityToken(
			claims: claims,
			signingCredentials: signingCredentials,
			expires: DateTime.UtcNow.AddHours(_jwtOptions.ExpiresHours));

		string? tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

		return tokenValue;
	}
}