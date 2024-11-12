using EventSourcing.Models;
using MediatR;

namespace EventSourcing.Features.GetPlayerById
{
    public record GetPlayerByIdQuery(int Id) : IRequest<Player?>;
}
