namespace WebAPI.Contracts.Authentication
{
    public record AuthResponse(
        string Id,
        string? Email,
        string FirstName,
        string LastName,
        string Token,
        string RefreshToken,
        int ExpiresIn,
        DateTime RefreshTokenExpiration
        );
}
