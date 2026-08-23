namespace WebAPI.Contracts.Questions
{
    public class QuestionsRequestValidator : AbstractValidator<QuestionsRequest>
    {
        public QuestionsRequestValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty()
                .Length(3, 1000);

            RuleFor(x => x.Answers)
                .NotNull();

            RuleFor(x => x.Answers)
                .Must(x => x.Count > 1)
                .WithMessage("Answers must contain at least 2 items.")
                .When(x => x.Answers != null);

            RuleFor(x => x.Answers)
                .Must(x => x.Distinct().Count() == x.Count)
                .WithMessage("Answers must be unique.")
                .When(x => x.Answers != null);
        }
    }
}
