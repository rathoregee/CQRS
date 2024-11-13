using EventSourcing.Database;
using EventSourcing.Models;
using EventSourcing.Repository;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Optional;

namespace EventSourcing.Features.GetPlayerById
{
    public class GetPlayerByIdQueryHandler : IRequestHandler<GetPlayerByIdQuery, Option<Player>>
    {
        private readonly IRepository _repository;
        public GetPlayerByIdQueryHandler(IRepository repo)
        {
            _repository = repo;
        }
        public async Task<Option<Player>> Handle(GetPlayerByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetPlayer(request.Id);
        }
    }
}