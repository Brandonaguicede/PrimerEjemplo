using System.Text.Json.Serialization;

namespace HackerRank1.Application.DTO;

public class TokenResponse
{
    public TokenResponse(string token)
    {
        Token = token;
    }

    [JsonPropertyName("token")]
    public string Token { get; }
}
