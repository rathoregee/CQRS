using EventSourcing.Database;
using EventSourcing.Models;
using EventSourcing.Repository;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EventSourcing.Features.CreatePlayer
{
    public class CreatePlayerCommandHandler : IRequestHandler<CreatePlayerCommand, int>
    {
        private readonly IRepository _repository;
        public CreatePlayerCommandHandler(IRepository repository)
        {
            _repository = repository;
        }
        public async Task<int> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
        {
            return await _repository.CreatePlayer(request);
        }
    }
}
