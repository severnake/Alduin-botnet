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
    public class GetAllRoleQueryHandler : IRequestHandler<GetAllRoleQuery, UserClaimDTO[]>
    {
        private readonly IUserClaimRepository _UserClaimRepository;
        public GetAllRoleQueryHandler(IUserClaimRepository UserClaimRepository)
        {
            _UserClaimRepository = UserClaimRepository;
        }

        public Task<UserClaimDTO[]> Handle(GetAllRoleQuery request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var Users = _UserClaimRepository.GetAll();

            return Task.FromResult(Users);
        }
    }
}
