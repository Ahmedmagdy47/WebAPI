namespace WebAPI.Errors
{
    public static class VoteErrors
    {
        //public static readonly Error VoteNotFound = 
        //   new("Vote.NotFound", "Vote not found");

        public static readonly Error InvalidQuestion =
           new("Vote.InvalidQuestions", "Invalid question selected", StatusCodes.Status400BadRequest);

        public static readonly Error DuplicatedVote =
           new("Vote.DuplicatedVote", "You have already voted for this poll", StatusCodes.Status409Conflict);

    }
}
