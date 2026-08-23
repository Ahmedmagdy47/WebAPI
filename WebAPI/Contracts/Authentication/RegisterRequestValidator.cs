namespace WebAPI.Contracts.Authentication
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .Matches(RegexPatterns.Password)
                .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character, and be at least 8 characters long");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .Length(2, 100);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .Length(2, 100);
        }
    }
}
