using WebAPI.Contracts.Answers;

namespace WebAPI.Contracts.Questions
{
    public record QuestionsResponse(
        int Id,
        string Content,
        IEnumerable<AnswerResponse> Answers
    );
}
