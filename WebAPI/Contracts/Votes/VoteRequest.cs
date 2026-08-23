namespace WebAPI.Contracts.Votes
{
    public record VoteRequest(
        IEnumerable<VoteAnswerRequest> Answers
    );
}
