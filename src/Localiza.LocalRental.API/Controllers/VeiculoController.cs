using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Localiza.LocalRental.API.Application.Commands;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Localiza.LocalRental.API.Application.Queries.Veiculo;

namespace Localiza.LocalRental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculoController : ControllerBase
    {
        private readonly IVeiculoQueries _veiculoQueries;
        private readonly IMediator _mediator;

        public VeiculoController(IVeiculoQueries veiculoQueries,
            IMediator mediator)
        {
            _veiculoQueries = veiculoQueries;
            _mediator = mediator;
        }

        /// <summary>
        /// Lista todos os veículos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<VeiculoResourceModel>> Get()
        {
            var result = _veiculoQueries.ListarTodosVeiculos();
            return Ok(result);
        }

        /// <summary>
        /// Retira um veículo
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("retirar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Retirar(RetirarVeiculoCommand command)
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
            return Ok();
        }

        /// <summary>
        /// Devolve um veículo
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("devolver")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Devolver(DevolverVeiculoCommand command)
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
            return Ok();
        }
    }
}
