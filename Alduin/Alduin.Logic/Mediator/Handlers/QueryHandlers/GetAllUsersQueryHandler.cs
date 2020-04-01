using Alduin.Logic.Interfaces.Repositories;
using Alduin.Logic.Mediator.Queries;
using Alduin.Shared.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Alduin.Logic.Mediator.Handlers.QueryHandlers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, UserDTO[]>
    {
        private readonly IUserRepository _UserRepository;
        public GetAllUsersQueryHandler(IUserRepository BotRepository)
        {
            _UserRepository = BotRepository;
        }

        public Task<UserDTO[]> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var users = _UserRepository.GetAll();

            return Task.FromResult(users);
        }
    }
}
