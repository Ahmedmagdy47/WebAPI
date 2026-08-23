using Microsoft.AspNetCore.RateLimiting;
using WebAPI.Contracts.Votes;

namespace WebAPI.Controllers
{
    [Route("api/polls/{pollId}/vote")]
    [ApiController]
    [Authorize(Roles = DefaultRoles.Member)]
    [EnableRateLimiting("concurrency")]
    public class VotesController(IQuestionServices questionServices, IVoteService voteServices) : ControllerBase

    {
        private readonly IQuestionServices _questionServices = questionServices;
        private readonly IVoteService _voteServices = voteServices;

        [HttpGet("")]
        public async Task<IActionResult> Start([FromRoute] int pollId, CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();

            var result = await _questionServices.GetAvailableAsync(pollId, userId!, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpPost("")]
        public async Task<IActionResult> Vote([FromRoute] int pollId, [FromBody] VoteRequest request, CancellationToken cancellationToken)
        {
            var result = await _voteServices.AddAsync(pollId, User.GetUserId()!, request, cancellationToken);
            return result.IsSuccess ? Created() : result.ToProblem();
        }
    }
}
