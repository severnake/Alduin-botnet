using Alduin.DataAccess.Entities;
using Alduin.Logic.DTOs;
using Alduin.Logic.Interfaces.Repositories;
using Alduin.Logic.Mediator.Commands;
using Alduin.Shared.DTOs;
using MediatR;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Alduin.Logic.Mediator.Handlers.CommandHandlers
{
    public class ChangeClaimCommandHandler : IRequestHandler<ChangeClaimCommand, ActionResult>
    {
        internal static ISession _session;
        private readonly IUserClaimRepository _iuserclaimrepository;
        public ChangeClaimCommandHandler(ISession session, IUserClaimRepository iuserclaimrepository)
        {
            _session = session;
            _iuserclaimrepository = iuserclaimrepository;
        }

        public async Task<ActionResult> Handle(ChangeClaimCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var u = _iuserclaimrepository.GetByUserId(request.UserId);
            var userid = 0;
            for (var i = 0; i < u.Length -1; i++)
            {
                if(u[i].ClaimType == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
                    userid = u[i].Id;
            }
            UserClaimEntity previous = _session.Get<UserClaimEntity>(userid);
            previous.ClaimValue = request.ClaimValue;
            previous.ModificationDateUTC = DateTime.UtcNow;
            using (var trans = _session.BeginTransaction())
            {
                _session.Update(previous);
                trans.Commit();
            }

            return new ActionResult { Suceeded = true };
        }
    }
}
