using MediatR;
using Microsoft.AspNetCore.Mvc;
using SistemaBancario.Exceptions;
using SistemaBancario.Service.Requester;

namespace SistemaBancario.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContaCorrenteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("movimentacao")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddMovimento([FromBody] CreateMovimentoRequest command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(new { IdMovimento = result });
            }
            catch (BusinessException ex)
            {
                return BadRequest(new { Message = ex.Message, Type = ex.ErrorCode });
            }
        }

        [HttpGet("{id}/saldo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetSaldo(string id)
        {
            try
            {
                var query = new SaldoRequest { IdContaCorrente = id };
                    var result = await _mediator.Send(query);
                return Ok(result);  
            }
            catch (BusinessException ex)
            {
                    return BadRequest(new { Message = ex.Message, Type = ex.ErrorCode });
            }
        }
    }
}
