using Xunit;
using Moq;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using EventSourcing.Controllers;
using EventSourcing.Features.CreatePlayer;
using EventSourcing.Features.GetPlayerById;
using EventSourcing.Models;
using Optional;
using System.Numerics;

namespace EventSourcing.Tests.Controllers
{
    public class PlayerControllerTests
    {
        private readonly Mock<ILogger<PlayerController>> _mockLogger;
        private readonly Mock<ISender> _mockSender;
        private readonly PlayerController _controller;

        public PlayerControllerTests()
        {
            _mockLogger = new Mock<ILogger<PlayerController>>();
            _mockSender = new Mock<ISender>();
            _controller = new PlayerController(_mockLogger.Object, _mockSender.Object);
        }

        [Fact]
        public async Task CreatePlayer_ValidRequest_ReturnsOk()
        {
            // Arrange
            var command = new CreatePlayerCommand { Name = "John", Level = 1 };
            _mockSender
                .Setup(sender => sender.Send(command, default))
                .ReturnsAsync(1);

            // Act
            var result = await _controller.CreatePlayer(command);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(1, okResult.Value);
        }

        [Fact]
        public async Task CreatePlayer_InvalidRequest_ReturnsBadRequest()
        {
            // Arrange
            var command = new CreatePlayerCommand { Name = "", Level = 0 };

            // Act
            var result = await _controller.CreatePlayer(command);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Contains("Property Name failed validation", badRequestResult.Value.ToString());
        }

        [Fact]
        public async Task GetPlayer_ValidId_ReturnsOkWithPlayer()
        {
            // Arrange
            var playerId = 1;
            var player = new Player { Id = 1, Name = "John", Level = 1 };
            _mockSender
                .Setup(sender => sender.Send(It.Is<GetPlayerByIdQuery>(q => q.Id == playerId), default))
                .ReturnsAsync(Option.Some(player));

            // Act
            var result = await _controller.GetPlayer(playerId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(player, okResult.Value);
        }

        [Fact]
        public async Task GetPlayer_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            var playerId = 0;

            // Act
            var result = await _controller.GetPlayer(playerId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("id is required and cannot be less than 1 value", badRequestResult.Value);
        }

        [Fact]
        public async Task GetPlayer_NonExistentId_ReturnsBadRequest()
        {
            // Arrange
            var playerId = 2;
            _mockSender
                .Setup(sender => sender.Send(It.Is<GetPlayerByIdQuery>(q => q.Id == playerId), default))
                .ReturnsAsync(Option.None<Player>());

            // Act
            var result = await _controller.GetPlayer(playerId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("player id does not exists!", badRequestResult.Value);
        }
    }
}
