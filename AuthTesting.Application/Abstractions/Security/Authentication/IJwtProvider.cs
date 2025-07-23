using AuthTesting.Domain.Entities;

namespace AuthTesting.Application.Abstractions.Security.Authentication;

public interface IJwtProvider
{
	string GetJwtToken(User user);
}