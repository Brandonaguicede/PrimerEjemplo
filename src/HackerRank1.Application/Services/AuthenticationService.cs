using HackerRank1.Application.Abstractions;
using HackerRank1.Application.Common;
using HackerRank1.Application.DTO;

namespace HackerRank1.Application.Services;

public interface IAuthenticationService
{
    Task<ServiceResult<TokenResponse>> LoginAsync(User user);
}

public class AuthenticationService : IAuthenticationService
{
    private readonly ITokenService _tokenService;

    public AuthenticationService(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public Task<ServiceResult<TokenResponse>> LoginAsync(User user)
    {
        if (user.Email == "admin" && user.Password == "1234")
        {
            var authenticatedUser = new User { Id = 1, Email = user.Email, Role = "admin" };
            return Task.FromResult(ServiceResult<TokenResponse>.Success(
                new TokenResponse(_tokenService.GenerateToken(authenticatedUser))));
        }

        return Task.FromResult(ServiceResult<TokenResponse>.Unauthorized("Invalid credentials."));
    }
}
