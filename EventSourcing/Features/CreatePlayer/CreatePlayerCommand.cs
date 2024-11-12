using MediatR;

namespace EventSourcing.Features.CreatePlayer
{
    public record CreatePlayerCommand: IRequest<int> // DTO Or request Object
    {
        public string Name { get; init; }
        public int Level { get; init; }
    }    
}
