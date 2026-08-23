namespace WebAPI.Contracts.Questions
{
    public record QuestionsRequest(
        string Content,
        List<string> Answers
    );
}
