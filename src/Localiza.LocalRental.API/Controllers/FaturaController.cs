using System.Threading.Tasks;
using Localiza.LocalRental.API.Application.Commands;
using Localiza.LocalRental.API.Application.Queries.Fatura;
using Localiza.LocalRental.Domain.Model.Fatura;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SC.SDK.NetStandard.DomainCore.Commands;

namespace Localiza.LocalRental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaturaController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IFaturaQueries _faturaQueries;

        public FaturaController(IMediator mediator, IFaturaQueries faturaQueries)
        {
            _mediator = mediator;
            _faturaQueries = faturaQueries;
        }

        /// <summary>
        /// Lista as faturas de um cliente
        /// </summary>
        /// <param name="clienteId">Id do cliente</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ListarPorCliente(string clienteId)
        {
            if (string.IsNullOrWhiteSpace(clienteId))
                return BadRequest();

            var result = _faturaQueries.ListarFaturasPorCliente(clienteId);
            return Ok(result);
        }

        /// <summary>
        /// Obtém uma fatura
        /// </summary>
        /// <param name="id">Id da Fatura</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult ObterPorId(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();

            var result = _faturaQueries.ObterFaturaPorId(id);
            return Ok(result);
        }

        /// <summary>
        /// Paga uma fatura gerada
        /// </summary>
        /// <param name="id">Id da fatura</param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("{id}/pagar")]
        public async Task<IActionResult> PagarFatura(string id, [FromBody] PagarFaturaCommand command)
        {
            command.FaturaId = id;
            command.Validate();

            if (command.Invalid)
            {
                return BadRequest(JsonConvert.SerializeObject(command.Notifications, Formatting.Indented));
            }
            var response = await _mediator.Send(command);
           
            if (!response.Success)
            {
                if (response.Reason == CommandResultError.ResourceNotFound)
                    return NotFound();

                return BadRequest(response.Message);
            }
            return Ok();
        }
    }
}
