using AuthTesting.Domain.Enums;

namespace AuthTesting.Domain.Entities;

public class User
{
	public Guid Id { get; init; }
	public string Name { get; init; } = string.Empty;
	public string PasswordHash { get; init; } = string.Empty;
	public Permission[] Permissions { get; set; } = [];
}