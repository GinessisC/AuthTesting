namespace AuthTesting.Contracts.Requests;

public sealed record RegisterRequest(
	string Name,
	string Password);