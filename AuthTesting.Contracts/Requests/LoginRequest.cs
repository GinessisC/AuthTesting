namespace AuthTesting.Contracts.Requests;

public sealed record LoginRequest(
	string Username,
	string Password);