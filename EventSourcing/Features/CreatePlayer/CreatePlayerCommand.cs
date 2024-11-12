using MediatR;

namespace EventSourcing.Features.CreatePlayer
{
    public record CreatePlayerCommand(string Name, int level): IRequest<int>; // DTO Or request Object
    
}
