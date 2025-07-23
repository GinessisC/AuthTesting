namespace AuthTesting.Infrastructure.Security.Abstractions;

public interface IPasswordHashing
{
	string Generate(string password);
	bool Verify(string password, string hashedPassword);
}