using AuthTesting.Application.Abstractions.Repositories;
using AuthTesting.Application.Abstractions.Security;
using AuthTesting.Application.Abstractions.Security.Authentication;
using AuthTesting.Application.Abstractions.Services;
using AuthTesting.Contracts.Requests;
using AuthTesting.Domain.Entities;

namespace AuthTesting.Application.Services;

public class UserService : IUserService
{
	private readonly IPasswordHashing _passwordHashing;
	private readonly IUserRepository _userRepository;
	private readonly IJwtProvider _jwtProvider;

	public UserService(
		IPasswordHashing passwordHashing,
		IUserRepository userRepository,
		IJwtProvider jwtProvider)
	{
		_passwordHashing = passwordHashing;
		_userRepository = userRepository;
		_jwtProvider = jwtProvider;
	}

	public async Task RegisterAsync(RegisterRequest userRequest)
	{
		string hashedPassword = _passwordHashing.Generate(userRequest.Password);

		await _userRepository.RegisterAsync(userRequest.Name, hashedPassword);
	}

	public async Task<string> LoginAsync(LoginRequest request)
	{
		User? user = await _userRepository.GetUserAsync(request.Username);

		if (user == null)
		{
			throw new Exception("No user found");
		}

		bool isVerified = _passwordHashing.Verify(request.Password, user.PasswordHash);

		if (isVerified is false)
		{
			throw new Exception("Invalid password or username");
		}

		string token = _jwtProvider.GetJwtToken(user);

		return token;
	}

	public async Task<User?> GetUserByNameAsync(string userName)
	{
		User? user = await _userRepository.GetUserAsync(userName);

		return user;
	}
}