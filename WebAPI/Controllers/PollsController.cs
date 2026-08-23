using Asp.Versioning;
using Microsoft.AspNetCore.RateLimiting;

namespace WebAPI.Controllers
{
    [ApiVersion(1, Deprecated = true)]
    [ApiVersion(2)]
    [Route("api/[controller]")]
    [ApiController]
    public class PollsController(IPollService pollServise) : ControllerBase
    {
        private readonly IPollService _pollService = pollServise;

        [HttpGet("")]
        [HasPermission(Permissions.GetPolls)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            return Ok(await _pollService.GetAllAsync(cancellationToken));
        }

        [MapToApiVersion(1)]
        [HttpGet("current")]
        [Authorize(Roles = DefaultRoles.Member)]
        [EnableRateLimiting(RateLimiters.UserLimiter)]
        public async Task<IActionResult> GetCurrentV1(CancellationToken cancellationToken)
        {
            return Ok(await _pollService.GetCurrentPollsAsyncV1(cancellationToken));
        }

        [MapToApiVersion(2)]
        [HttpGet("current")]
        [Authorize(Roles = DefaultRoles.Member)]
        [EnableRateLimiting(RateLimiters.UserLimiter)]
        public async Task<IActionResult> GetCurrentV2(CancellationToken cancellationToken)
        {
            return Ok(await _pollService.GetCurrentPollsAsyncV2(cancellationToken));
        }

        [HttpGet("{id}")]
        [HasPermission(Permissions.GetPolls)]
        public async Task<IActionResult> Get([FromRoute] int id,
            CancellationToken cancellationToken)
        {
            var result = await _pollService.GetAsync(id, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpPost("")]
        [HasPermission(Permissions.AddPolls)]
        public async Task<IActionResult> Add([FromBody] PollRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _pollService.AddAsync(request, cancellationToken);

            return result.IsSuccess
                ? CreatedAtAction(nameof(Get), new { id = result.Value.Id }, result.Value)
                : result.ToProblem();
        }

        [HttpPut("{id}")]
        [HasPermission(Permissions.UpdatePolls)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PollRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _pollService.UpdateAsync(id, request, cancellationToken);

            return result.IsSuccess ? NoContent() : result.ToProblem();
        }

        [HttpDelete("{id}")]
        [HasPermission(Permissions.DeletePolls)]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _pollService.DeleteAsync(id, cancellationToken);
            return result.IsSuccess ? Ok() : result.ToProblem();
        }

        [HttpPut("{id}/toggle-publish")]
        [HasPermission(Permissions.UpdatePolls)]
        public async Task<IActionResult> TogglePublish([FromRoute] int id, CancellationToken cancellationToken)
        {
            var isUpdated = await _pollService.TogglePublishStatusAsync(id, cancellationToken);

            return isUpdated.IsSuccess ? Ok() : isUpdated.ToProblem();
        }

    }
}
