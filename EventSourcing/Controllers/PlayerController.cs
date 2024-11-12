using EventSourcing.Features.CreatePlayer;
using EventSourcing.Features.GetPlayerById;
using EventSourcing.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly ILogger<PlayerController> _logger;
        private readonly ISender _sender;
        public PlayerController(ILogger<PlayerController> logger, ISender sender)
        {
            _logger = logger;
            _sender = sender;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreatePlayer(CreatePlayerCommand command)
        {
            var playerId = await _sender.Send(command);

            _logger.LogInformation("request completed");

            return Ok(playerId);
        }

        [HttpGet("id")]
        public async Task<ActionResult<Player>> GetPlayer(int id)
        {
            var player = await _sender.Send(new GetPlayerByIdQuery(id));

            if (player == null) {

                return NotFound();
            }

            _logger.LogInformation("request completed");

            return Ok(player);
        }
    }
}
