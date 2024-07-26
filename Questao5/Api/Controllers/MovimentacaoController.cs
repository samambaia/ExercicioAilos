using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;

namespace Questao5.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovimentacaoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MovimentacaoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("movimento")]
        public async Task<IActionResult> Post([FromBody] MovimentacaoCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
