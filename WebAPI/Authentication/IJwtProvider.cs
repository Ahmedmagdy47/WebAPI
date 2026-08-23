namespace WebAPI.Authentication
{
    public interface IJwtProvider
    {
        //Tuple return two values
        (string token, int expiresIn) GenerateToken(ApplicationUser user, IEnumerable<string> roles, IEnumerable<string> premissions);
        string? ValidateToken(string token);
    }
}
