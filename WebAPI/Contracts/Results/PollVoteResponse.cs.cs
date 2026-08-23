namespace WebAPI.Contracts.Results
{
    public record PollVoteResponse(
        string title,
        IEnumerable<VoteResponse> votes
    );
}
