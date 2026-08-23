using Hangfire;

namespace WebAPI.Services
{
    public class PollServise(
        ApplicationDbContext context,
        INotificationService notificationService) : IPollService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly INotificationService _notificationService = notificationService;

        public async Task<IEnumerable<PollResponse>> GetAllAsync(CancellationToken cancellationToken = default) =>
            await _context.Polls
            .AsNoTracking()
            .ProjectToType<PollResponse>()
            .ToListAsync(cancellationToken);

        public async Task<IEnumerable<PollResponse>> GetCurrentPollsAsyncV1(CancellationToken cancellationToken = default) =>
            await _context.Polls
            .Where(x => x.IsPublished && x.StartsAt <= DateOnly.FromDateTime(DateTime.UtcNow) && x.EndsAt >= DateOnly.FromDateTime(DateTime.UtcNow))
            .AsNoTracking()
            .ProjectToType<PollResponse>()
            .ToListAsync(cancellationToken);

        public async Task<IEnumerable<PollResponseV2>> GetCurrentPollsAsyncV2(CancellationToken cancellationToken = default) =>
            await _context.Polls
            .Where(x => x.IsPublished && x.StartsAt <= DateOnly.FromDateTime(DateTime.UtcNow) && x.EndsAt >= DateOnly.FromDateTime(DateTime.UtcNow))
            .AsNoTracking()
            .ProjectToType<PollResponseV2>()
            .ToListAsync(cancellationToken);

        public async Task<Result<PollResponse>> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            var poll = await _context.Polls.FindAsync(id, cancellationToken);

            return poll is not null
                ? Result.Success(poll.Adapt<PollResponse>())
                : Result.Failure<PollResponse>(PollErrors.PollNotFound);
        }

        public async Task<Result<PollResponse>> AddAsync(PollRequest request, CancellationToken cancellationToken = default)
        {
            var existingPoll = await _context.Polls.AnyAsync(x => x.Title == request.Title, cancellationToken: cancellationToken);

            if (existingPoll)
                return Result.Failure<PollResponse>(PollErrors.DuplicatedPollTitle);

            var poll = request.Adapt<Poll>();

            await _context.AddAsync(poll, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(poll.Adapt<PollResponse>());
        }

        public async Task<Result> UpdateAsync(int id, PollRequest request, CancellationToken cancellationToken = default)
        {
            var existingPoll = await _context.Polls.AnyAsync(x => x.Title == request.Title && x.Id != id, cancellationToken: cancellationToken);

            if (existingPoll)
                return Result.Failure<PollResponse>(PollErrors.DuplicatedPollTitle);

            var currentPoll = await _context.Polls.FindAsync(id, cancellationToken);

            if (currentPoll == null)
                return Result.Failure(PollErrors.PollNotFound);

            currentPoll = request.Adapt(currentPoll);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var existingPoll = await _context.Polls.FindAsync(id, cancellationToken);

            if (existingPoll == null)
                return Result.Failure(PollErrors.PollNotFound);

            _context.Remove(existingPoll);

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result> TogglePublishStatusAsync(int id, CancellationToken cancellationToken = default)
        {
            var existingPoll = await _context.Polls.FindAsync(id, cancellationToken);

            if (existingPoll is null)
                return Result.Failure(PollErrors.PollNotFound);

            existingPoll.IsPublished = !existingPoll.IsPublished;

            await _context.SaveChangesAsync(cancellationToken);

            if (existingPoll.IsPublished && existingPoll.StartsAt == DateOnly.FromDateTime(DateTime.UtcNow))
                BackgroundJob.Enqueue(() => _notificationService.SendNewPollNotificationAsync(existingPoll.Id));

            return Result.Success();
        }
    }
}
