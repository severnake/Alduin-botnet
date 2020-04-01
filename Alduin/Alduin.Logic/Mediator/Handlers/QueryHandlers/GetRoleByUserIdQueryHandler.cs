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
    public class GetRoleByUserIdQueryHandler : IRequestHandler<GetRoleByUserIdQuery, UserClaimDTO[]>
    {
        private readonly IUserClaimRepository _userClaimRepository;
        public GetRoleByUserIdQueryHandler(IUserClaimRepository userClaimRepository)
        {
            _userClaimRepository = userClaimRepository;
        }

        public Task<UserClaimDTO[]> Handle(GetRoleByUserIdQuery request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var role = _userClaimRepository.GetByUserId(request.Id);
            return Task.FromResult(role);
        }
    }
}
