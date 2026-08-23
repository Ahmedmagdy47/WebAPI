namespace WebAPI.Contracts.Votes
{
    public class VoteAnswerRequsetValidator : AbstractValidator<VoteAnswerRequest>
    {
        public VoteAnswerRequsetValidator()
        {
            RuleFor(x => x.QuestionId)
                .GreaterThan(0);

            RuleFor(x => x.AnswerId)
                .GreaterThan(0);
        }
    }
}
