using EventSourcing.Database;
using EventSourcing.Features.CreatePlayer;
using EventSourcing.Models;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace EventSourcing.Repository
{
    public class Repository : IRepository
    {
        private readonly PlayerContext _playerContext;
        public Repository(PlayerContext context) { _playerContext = context; }
        public async Task<Option<Player>> GetPlayer(int id)
        {
            var player = await _playerContext.Players.FirstOrDefaultAsync(x => x.Id == id);

            return (player != null) ? Option.Some(player) : Option.None<Player>();
        }

        public async Task<int> CreatePlayer(CreatePlayerCommand request)
        {
            
            _playerContext.Players.Add(new Player { Name = request.Name, Level = request.Level });

            await _playerContext.SaveChangesAsync();
            
            var id = _playerContext.Players.Max(x => x.Id);

            if (id != null)
            {
                return id.Value;
            }
            return -1;
        }
    }
}
