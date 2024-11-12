using EventSourcing.Models;
using MediatR;
using Optional;

namespace EventSourcing.Features.GetPlayerById
{
    public record GetPlayerByIdQuery(int Id) : IRequest<Option<Player>>;
}
