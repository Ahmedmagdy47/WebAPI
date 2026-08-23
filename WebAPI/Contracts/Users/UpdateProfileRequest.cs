namespace WebAPI.Contracts.Users
{
    public record UpdateProfileRequest(
        string FirstName,
        string LastName
    );
}
