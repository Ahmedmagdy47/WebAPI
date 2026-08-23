namespace WebAPI.Contracts.Roles
{
    public record RoleRequest(
        string Name,
        IList<string> Permissions
    );
}
