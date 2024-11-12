using EventSourcing.Database;
using EventSourcing.Models;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Optional;

namespace EventSourcing.Features.GetPlayerById
{
    public class GetPlayerByIdQueryHandler : IRequestHandler<GetPlayerByIdQuery, Option<Player>>
    {
        public async Task<Option<Player>> Handle(GetPlayerByIdQuery request, CancellationToken cancellationToken)
        {
            var items = ContextHelper.Players.Where(x => x.Id == request.Id);

            var player = items.FirstOrDefault();

            await Task.Delay(0, cancellationToken);

            return (player != null) ? Option.Some(player) : Option.None<Player>();
        }
    }
}