namespace WebAPI.Contracts.Authentication
{
    public record RefreshTokenRequest(string Token, string RefreshToken);
}
