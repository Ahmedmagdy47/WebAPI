using WebAPI.Contracts.Common;
using WebAPI.Contracts.Questions;

namespace WebAPI.Services
{
    public interface IQuestionServices
    {
        Task<Result<PaginatedList<QuestionsResponse>>> GetAllAsync(int pollId, RequestFilters filters, CancellationToken cancellationToken);
        Task<Result<IEnumerable<QuestionsResponse>>> GetAvailableAsync(int pollId, string userId, CancellationToken cancellationToken);
        Task<Result<QuestionsResponse>> GetAsync(int pollId, int id, CancellationToken cancellationToken = default);
        Task<Result<QuestionsResponse>> AddAsync(int pollId, QuestionsRequest request, CancellationToken cancellationToken = default);
        Task<Result> UpdateAsync(int pollId, int id, QuestionsRequest request, CancellationToken cancellationToken = default);
        Task<Result> ToggleStatusAsync(int pollId, int id, CancellationToken cancellationToken = default);
    }
}
