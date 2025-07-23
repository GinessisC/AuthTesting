using AuthTesting.Application.Abstractions.Security;

namespace AuthTesting.Infrastructure.Security.Hashing;

public class PasswordHashing : IPasswordHashing
{
	public string Generate(string password)
	{
		string? hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(password);

		return hashedPassword;
	}

	public bool Verify(string password, string hashedPassword)
	{
		return BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
	}
}