namespace WebAPI.Contracts.Users
{
    public class UpdateProfileRequestValidator : AbstractValidator<UpdateProfileRequest>
    {
        public UpdateProfileRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .Length(2, 100);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .Length(2, 100);
        }
    }
}
