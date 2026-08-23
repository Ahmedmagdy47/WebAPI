namespace WebAPI.Contracts.Authentication
{
    public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
    {
        public ResetPasswordRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Code)
                .NotEmpty();

            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .MinimumLength(8)
                .Matches(RegexPatterns.Password)
                .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character, and be at least 8 characters long");

        }
    }
}
