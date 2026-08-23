using Microsoft.Extensions.Caching.Hybrid;
using System.Linq.Dynamic.Core;
using WebAPI.Contracts.Answers;
using WebAPI.Contracts.Common;
using WebAPI.Contracts.Questions;

namespace WebAPI.Services
{
    public class QuestionServices(ApplicationDbContext context,
        HybridCache hybridCache,
        ILogger<QuestionServices> logger) : IQuestionServices
    {
        private readonly ApplicationDbContext _context = context;
        private readonly HybridCache _hybridCache = hybridCache;
        private readonly ILogger<QuestionServices> logger = logger;

        private const string _cachePrefix = "availableQuestions";

        public async Task<Result<IEnumerable<QuestionsResponse>>> GetAvailableAsync(int pollId, string userId, CancellationToken cancellationToken)
        {
            var hasVoted = await _context.Votes.AnyAsync(x => x.PollId == pollId && x.UserId == userId, cancellationToken: cancellationToken);

            if (hasVoted)
                return Result.Failure<IEnumerable<QuestionsResponse>>(VoteErrors.DuplicatedVote);

            var IsPollExists = await _context.Polls.AnyAsync(x => x.Id == pollId && x.IsPublished && x.StartsAt <= DateOnly.FromDateTime(DateTime.UtcNow) && x.EndsAt >= DateOnly.FromDateTime(DateTime.UtcNow), cancellationToken: cancellationToken);

            if (!IsPollExists)
                return Result.Failure<IEnumerable<QuestionsResponse>>(PollErrors.PollNotFound);

            var cacheKey = $"{_cachePrefix}_{pollId}";

            var questions = await _hybridCache.GetOrCreateAsync<IEnumerable<QuestionsResponse>>(
                cacheKey,
                async cacheEntry => await _context.Questions
                .Where(x => x.PollId == pollId && x.IsActive)
                .Include(x => x.Answers)
                .Select(q => new QuestionsResponse(
                    q.Id,
                    q.Content,
                    q.Answers.Where(a => a.IsActive).Select(a => new AnswerResponse(a.Id, a.Content))
                ))
                .AsNoTracking()
                .ToListAsync(cancellationToken)
            );

            return Result.Success(questions!);
        }
        public async Task<Result<PaginatedList<QuestionsResponse>>> GetAllAsync(int pollId, RequestFilters filters, CancellationToken cancellationToken)
        {
            var pollIsExist = await _context.Polls.AnyAsync(x => x.Id == pollId, cancellationToken: cancellationToken);

            if (!pollIsExist)
                return Result.Failure<PaginatedList<QuestionsResponse>>(QuestionErrors.QuestionNotFound);

            var query = _context.Questions
                .Where(x => x.PollId == pollId);

            if (!string.IsNullOrEmpty(filters.SearchValue))
            {
                query = query.Where(x => x.Content.Contains(filters.SearchValue));
            }

            if (!string.IsNullOrEmpty(filters.SortColumn))
            {
                query = query.OrderBy($"{filters.SortColumn} {filters.SortDirection}");
            }

            var source = query
                            .Include(x => x.Answers)
                            .ProjectToType<QuestionsResponse>()
                            .AsNoTracking();

            var questions = await PaginatedList<QuestionsResponse>.CreateAsync(source, filters.PageNumber, filters.PageSize, cancellationToken);

            return Result.Success(questions);
        }

        public async Task<Result<QuestionsResponse>> GetAsync(int pollId, int id, CancellationToken cancellationToken = default)
        {
            var question = await _context.Questions
                .Where(x => x.PollId == pollId && x.Id == id)
                .Include(x => x.Answers)
                .ProjectToType<QuestionsResponse>()
                .AsNoTracking()
                .SingleOrDefaultAsync(cancellationToken);

            if (question is null)
                return Result.Failure<QuestionsResponse>(QuestionErrors.QuestionNotFound);


            return Result.Success(question);
        }

        public async Task<Result<QuestionsResponse>> AddAsync(int pollId, QuestionsRequest request, CancellationToken cancellationToken = default)
        {
            var pollIsExist = await _context.Polls.AnyAsync(x => x.Id == pollId, cancellationToken: cancellationToken);

            if (!pollIsExist)
                return Result.Failure<QuestionsResponse>(QuestionErrors.QuestionNotFound);

            var questionIsExist = await _context.Questions.AnyAsync(x => x.Content == request.Content && x.PollId == pollId, cancellationToken: cancellationToken);

            if (questionIsExist)
                return Result.Failure<QuestionsResponse>(QuestionErrors.DuplicatedQuestionContent);

            //var question = request.Adapt<Question>();
            //question.PollId = pollId;

            //request.Answers.ForEach(answer => question.Answers.Add(new Answer { Content = answer }));

            var question = new Question
            {
                Content = request.Content.Trim(),
                PollId = pollId,
                Answers = request.Answers
            .Select(answer => answer.Trim())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .Select(answerContent => new Answer { Content = answerContent })
            .ToList()
            };


            await _context.AddAsync(question, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            await _hybridCache.RemoveAsync($"{_cachePrefix}_{pollId}", cancellationToken);

            return Result.Success(question.Adapt<QuestionsResponse>());
        }

        public async Task<Result> UpdateAsync(int pollId, int id, QuestionsRequest request, CancellationToken cancellationToken = default)
        {
            var questionIsExist = await _context.Questions.AnyAsync(x => x.PollId == pollId
                && x.Id != id
                && x.Content == request.Content,
                cancellationToken
            );

            if (questionIsExist)
                return Result.Failure(QuestionErrors.DuplicatedQuestionContent);

            var question = await _context.Questions
                .Include(x => x.Answers)
                .SingleOrDefaultAsync(x => x.PollId == pollId && x.Id == id, cancellationToken: cancellationToken);

            if (question is null)
                return Result.Failure(QuestionErrors.QuestionNotFound);

            question.Content = request.Content;

            //current answers
            var currentAnswers = question.Answers.Select(x => x.Content).ToList();

            //add new answers
            var newAnswers = request.Answers.Except(currentAnswers).ToList();

            newAnswers.ForEach(answer =>
            {
                question.Answers.Add(new Answer { Content = answer });
            });

            question.Answers.ToList().ForEach(answer =>
            {
                answer.IsActive = request.Answers.Contains(answer.Content);
            });

            await _context.SaveChangesAsync(cancellationToken);

            await _hybridCache.RemoveAsync($"{_cachePrefix}_{pollId}", cancellationToken);

            return Result.Success();
        }

        public async Task<Result> ToggleStatusAsync(int pollId, int id, CancellationToken cancellationToken = default)
        {
            var isExist = await _context.Questions.SingleOrDefaultAsync(x => x.PollId == pollId && x.Id == id, cancellationToken: cancellationToken);

            if (isExist is null)
                return Result.Failure(QuestionErrors.QuestionNotFound);

            isExist.IsActive = !isExist.IsActive;

            await _context.SaveChangesAsync(cancellationToken);

            await _hybridCache.RemoveAsync($"{_cachePrefix}_{pollId}", cancellationToken);

            return Result.Success();
        }
    }
}
