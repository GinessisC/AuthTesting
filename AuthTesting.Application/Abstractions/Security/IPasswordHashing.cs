namespace AuthTesting.Application.Abstractions.Security;

public interface IPasswordHashing
{
	string Generate(string password);
	bool Verify(string password, string hashedPassword);
}