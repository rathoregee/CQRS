using EventSourcing.Features.CreatePlayer;
using EventSourcing.Features.GetPlayerById;
using EventSourcing.Models;
using EventSourcing.Validation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Optional;
using Optional.Unsafe;
using System.Text;

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
            var validator = new CreatePlayerValidator();

            var vaildation = await validator.ValidateAsync(command);

            if (!vaildation.IsValid)
            {
                var message = GetValidationResults(vaildation);

                return BadRequest(message);
            }

            var playerId = await _sender.Send(command);

            _logger.LogInformation("post request completed");

            return Ok(playerId);
        }  

        [HttpGet("id")]
        public async Task<ActionResult<Option<Player>>> GetPlayer(int id)
        {
            if (id < 1)
            {
                return BadRequest("id is required and cannot be less than 1 value");
            }

            var option = await _sender.Send(new GetPlayerByIdQuery (id));

            _logger.LogInformation("get request completed");

            return option.HasValue ? Ok(option.ValueOrDefault()) : BadRequest("player id does not exists!");
        }

        private static string GetValidationResults(ValidationResult results)
        {
            var sb = new StringBuilder();
            foreach (var failure in results.Errors)
            {
                sb.AppendLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
            }

            return sb.ToString();
        }
    }
}
