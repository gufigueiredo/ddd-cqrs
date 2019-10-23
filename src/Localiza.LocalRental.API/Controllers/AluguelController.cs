using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Localiza.LocalRental.API.Application.Commands;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Localiza.LocalRental.Domain.Model.Aluguel;
using Microsoft.AspNetCore.Http;
using Localiza.LocalRental.API.Application.Queries.Aluguel;

namespace Localiza.LocalRental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AluguelController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAluguelQueries _aluguelQueries;

        public AluguelController(IAluguelQueries aluguelQueries, IMediator mediator)
        {
            _mediator = mediator;
            _aluguelQueries = aluguelQueries;
        }

        /// <summary>
        /// Lista todos os aluguéis
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<Aluguel>> Get()
        {
            var result = _aluguelQueries.ListarTodosAlugueis();
            return Ok(result);
        }

        /// <summary>
        /// Obtém um aluguel
        /// </summary>
        /// <param name="id">Id do aluguel</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = nameof(GetPedidoAluguelPorId))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetPedidoAluguelPorId(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();

            var aluguel = _aluguelQueries.ObterAluguel(id);
            if (aluguel == null)
                return NotFound();

            return Ok(aluguel);
        }

        /// <summary>
        /// Cria um novo aluguel
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] AlugarVeiculoCommand command)
        {
            command.Validate();
            if (command.Invalid)
            {
                return BadRequest(JsonConvert.SerializeObject(command.Notifications, Formatting.Indented));
            }
            var response = await _mediator.Send(command);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return CreatedAtAction(nameof(GetPedidoAluguelPorId), new { id = response.ResourceId }, Guid.Parse(response.ResourceId));
        }

        /// <summary>
        /// Fecha/Finaliza um aluguel
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("{id}/fechar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Fechar([FromBody] FecharAluguelCommand command)
        {
            command.Validate();
            if (command.Invalid)
            {
                return BadRequest(JsonConvert.SerializeObject(command.Notifications, Formatting.Indented));
            }
            var response = await _mediator.Send(command);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return CreatedAtAction(nameof(GetPedidoAluguelPorId), new { id = response.ResourceId }, Guid.Parse(response.ResourceId));
        }

        
    }
}
