using EventSourcing.Database;
using EventSourcing.Models;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EventSourcing.Features.GetPlayerById
{
    public class GetPlayerByIdQueryHandler : IRequestHandler<GetPlayerByIdQuery, Player?>
    {       
        public async Task<Player?> Handle(GetPlayerByIdQuery request, CancellationToken cancellationToken)
        {

            var player = ContextHelper.Players.Where(x => x.Id == request.Id);

           
            await Task.Delay(500);

            return player.FirstOrDefault();
        }
    }
}