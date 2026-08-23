using WebAPI.Contracts.Common;
using WebAPI.Contracts.Questions;

namespace WebAPI.Controllers
{
    [Route("api/polls/{pollId}/[controller]")]
    [ApiController]
    public class QuestionsController(IQuestionServices questionServices) : ControllerBase
    {
        private readonly IQuestionServices _questionServices = questionServices;

        [HttpGet("")]
        [HasPermission(Permissions.GetQuestions)]
        public async Task<IActionResult> GetAllAsync([FromRoute] int pollId, [FromQuery] RequestFilters filters, CancellationToken cancellationToken)
        {
            var result = await _questionServices.GetAllAsync(pollId, filters, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpGet("{id}")]
        [HasPermission(Permissions.GetQuestions)]
        public async Task<IActionResult> Get([FromRoute] int pollId, [FromRoute] int id, CancellationToken cancellationToken = default)
        {
            var result = await _questionServices.GetAsync(pollId, id, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpPost("")]
        [HasPermission(Permissions.AddQuestions)]
        public async Task<IActionResult> AddAsync([FromRoute] int pollId, [FromBody] QuestionsRequest request, CancellationToken cancellationToken)
        {
            var result = await _questionServices.AddAsync(pollId, request, cancellationToken);

            return result.IsSuccess
                ? CreatedAtAction(nameof(Get), new { pollId, id = result.Value.Id }, result.Value)
                : result.ToProblem();

        }

        [HttpPut("{id}")]
        [HasPermission(Permissions.UpdateQuestions)]
        public async Task<IActionResult> UpdateAsync([FromRoute] int pollId, [FromRoute] int Id, [FromBody] QuestionsRequest request, CancellationToken cancellationToken)
        {
            var result = await _questionServices.UpdateAsync(pollId, Id, request, cancellationToken);

            return result.IsSuccess ? NoContent() : result.ToProblem();
        }

        [HttpPut("{id}/toggle-status")]
        [HasPermission(Permissions.UpdateQuestions)]
        public async Task<IActionResult> ToggleStatusAsync([FromRoute] int pollId, [FromRoute] int id, CancellationToken cancellationToken = default)
        {
            var isActive = await _questionServices.ToggleStatusAsync(pollId, id, cancellationToken);
            return isActive.IsSuccess ? Ok() : isActive.ToProblem();
        }

    }
}
