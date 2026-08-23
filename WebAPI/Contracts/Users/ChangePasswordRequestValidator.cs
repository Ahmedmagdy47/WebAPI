namespace WebAPI.Contracts.Users
{
    public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
    {
        public ChangePasswordRequestValidator()
        {
            RuleFor(x => x.CurrentPassword)
                .NotEmpty();

            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .Matches(RegexPatterns.Password)
                .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character, and be at least 8 characters long")
                .NotEqual(x => x.CurrentPassword)
                .WithMessage("New password cannot be the same as the current password");
        }
    }
}
