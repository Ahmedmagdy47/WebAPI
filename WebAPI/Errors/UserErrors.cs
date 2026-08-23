namespace WebAPI.Errors
{
    public record UserErrors
    {
        public static readonly Error InvalidCredentials =
           new("User.InvalidCredentials", "Invalid Login", StatusCodes.Status401Unauthorized);

        public static readonly Error DisabledUser =
           new("User.DisabledUser", "User is disabled", StatusCodes.Status401Unauthorized);

        public static readonly Error LockedOutUser =
           new("User.LockedOutUser", "User is locked out", StatusCodes.Status401Unauthorized);

        public static readonly Error InvalidJwtToken =
           new("User.InvalidJwtToken", "Invalid JWT token", StatusCodes.Status401Unauthorized);

        public static readonly Error InvalidRefreshToken =
           new("User.InvalidRefreshToken", "Invalid refresh token", StatusCodes.Status401Unauthorized);

        public static readonly Error DuplicatedEmail =
           new("User.DuplicatedEmail", "Email already exists", StatusCodes.Status409Conflict);

        public static readonly Error EmailNotConfirmed =
           new("User.EmailNotConfirmed", "Email is not confirmed", StatusCodes.Status401Unauthorized);

        public static readonly Error InvalidCode =
           new("User.InvalidCode", "Invalid confirmation code", StatusCodes.Status401Unauthorized);

        public static readonly Error DuplicaterComfirmation =
           new("User.EmailAlreadyConfirmed", "Email is already confirmed", StatusCodes.Status400BadRequest);

        public static readonly Error UserNotFound =
           new("User.UserNotFound", "User is not found", StatusCodes.Status404NotFound);

        public static readonly Error InvalidRoles =
           new("User.InvalidRoles", "Invalid roles", StatusCodes.Status400BadRequest);

    }
}
