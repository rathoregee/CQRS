using EventSourcing.Database;
using EventSourcing.Models;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EventSourcing.Features.CreatePlayer
{
    public class CreatePlayerCommandHandler : IRequestHandler<CreatePlayerCommand, int>
    {
        public async Task<int> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
        {
            var rnd = new Random();

            int id = rnd.Next(1, int.MaxValue);

            ContextHelper.Players.Add(new Player { Id = id, Name = request.Name, Level = request.Level});

            await Task.Delay(0, cancellationToken);

            return id;
        }
    }
}
