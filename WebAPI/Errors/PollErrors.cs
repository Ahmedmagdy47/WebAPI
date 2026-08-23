namespace WebAPI.Errors
{
    public static class PollErrors
    {
        public static readonly Error PollNotFound =
           new("Poll.NotFound", "Poll not found", StatusCodes.Status404NotFound);

        public static readonly Error DuplicatedPollTitle =
           new("Poll.DuplicatedPollTitle", "Poll with this title already exists", StatusCodes.Status409Conflict);

    }
}
