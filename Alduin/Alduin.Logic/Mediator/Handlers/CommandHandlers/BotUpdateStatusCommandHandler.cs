using Alduin.DataAccess.Entities;
using Alduin.Logic.DTOs;
using Alduin.Logic.Mediator.Commands;
using MediatR;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Alduin.Logic.Mediator.Handlers.CommandHandlers
{
    public class BotUpdateStatusCommandHandler : IRequestHandler<UpdateBotStatusCommand, ActionResult>
    {
        internal static ISession _session;
        public BotUpdateStatusCommandHandler(ISession session)
        {
            _session = session;
        }

        public async Task<ActionResult> Handle(UpdateBotStatusCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            BotEntity botentity = _session.Get<BotEntity>(request.id);
            botentity.LastLoggedInUTC = DateTime.Now;
            using (var trans = _session.BeginTransaction())
            {
                _session.Update(botentity);
                trans.Commit();
            }
            return new ActionResult { Suceeded = true };
        }
    }
}
