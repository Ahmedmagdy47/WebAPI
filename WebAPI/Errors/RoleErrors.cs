namespace WebAPI.Errors
{
    public static class RoleErrors
    {
        public static readonly Error RoleNotFound =
           new("Role.RoleNotFound", "Role is not found", StatusCodes.Status404NotFound);

        public static readonly Error InvalidPermission =
           new("Role.InvalidJwtToken", "Invalid permission", StatusCodes.Status400BadRequest);

        public static readonly Error DuplicatedRole =
           new("Role.DuplicatedRole", "Role already exists", StatusCodes.Status409Conflict);
    }
}
