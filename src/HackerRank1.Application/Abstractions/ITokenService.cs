using HackerRank1.Application.DTO;

namespace HackerRank1.Application.Abstractions;

public interface ITokenService
{
    string GenerateToken(User user);
}
