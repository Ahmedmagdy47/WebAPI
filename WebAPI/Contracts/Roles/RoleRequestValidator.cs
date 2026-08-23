namespace WebAPI.Contracts.Roles
{
    public class RoleRequestValidator : AbstractValidator<RoleRequest>
    {
        public RoleRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(1, 200);

            RuleFor(x => x.Permissions)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Permissions)
                .Must(x => x.Distinct().Count() == x.Count)
                .WithMessage("You cannot add duplicated permissions for the same role")
                .When(x => x.Permissions != null);
        }
    }
}
