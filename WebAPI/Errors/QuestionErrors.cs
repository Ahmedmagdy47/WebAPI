namespace WebAPI.Errors
{
    public static class QuestionErrors
    {
        public static readonly Error QuestionNotFound =
           new("Question.NotFound", "Question not found", StatusCodes.Status404NotFound);

        public static readonly Error DuplicatedQuestionContent =
           new("Question.DuplicatedQuestionContent", "Question with this content already exists", StatusCodes.Status409Conflict);

    }
}
