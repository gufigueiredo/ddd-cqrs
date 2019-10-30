using Localiza.LocalRental.Infrastructure.Events;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Localiza.LocalRental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventStream _eventStream;

        public EventsController(IEventStream eventStream)
        {
            _eventStream = eventStream;
        }

        /// <summary>
        /// Lista todos os eventos ocorridos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var result = _eventStream.GetStream();
            return Ok(result);
        }
    }
}
