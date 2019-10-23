using System;
using System.Threading.Tasks;
using Localiza.LocalRental.API.Application.Commands;
using Localiza.LocalRental.API.Application.Queries.Cliente;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Localiza.LocalRental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IClienteQueries _clienteQueries;

        public ClienteController(IMediator mediator, IClienteQueries clienteQueries)
        {
            _mediator = mediator;
            _clienteQueries = clienteQueries;
        }

        /// <summary>
        /// Cadastra um novo cliente
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] CadastrarClienteCommand command)
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
            return CreatedAtAction(nameof(GetClientePorId), new { id = response.ResourceId }, Guid.Parse(response.ResourceId));
        }

        /// <summary>
        /// Obtém um cliente pelo seu ID
        /// </summary>
        /// <param name="id">Id do cliente</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = nameof(GetClientePorId))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetClientePorId(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();

            var cliente = _clienteQueries.ObterClientePorId(id);
            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }
    }
}
