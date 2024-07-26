using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Interfaces;
using Questao5.Application.Queries.Requests;
using Volo.Abp;

namespace Questao5.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly IMediatorHandler _mediator;

        public ContaCorrenteController(IMediatorHandler mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("saldo")]
        public async Task<IActionResult> GetSaldo([FromQuery] SaldoContaCorrenteQuery query)
        {
            try
            {
                var result = await _mediator.SendCommand(query);
                return Ok(result);
            }
            catch (BusinessException ex)
            {
                return BadRequest(new { message = ex.Message, type = ex.GetType().Name });
            }
        }

    }
}
