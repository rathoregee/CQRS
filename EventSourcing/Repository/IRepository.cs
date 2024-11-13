using EventSourcing.Features.CreatePlayer;
using EventSourcing.Models;
using Optional;

namespace EventSourcing.Repository
{
    public interface IRepository
    {
        Task<int> CreatePlayer(CreatePlayerCommand request);
        Task<Option<Player>> GetPlayer(int id);
    }
}