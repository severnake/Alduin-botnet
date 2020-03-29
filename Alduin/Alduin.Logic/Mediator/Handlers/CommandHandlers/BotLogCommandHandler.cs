using Alduin.DataAccess.Entities;
using Alduin.Logic.DTOs;
using Alduin.Logic.Mediator.Commands;
using Alduin.Shared.Interfaces.UnitOfWork;
using MediatR;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Alduin.Logic.Mediator.Handlers.CommandHandlers
{
    public class BotLogCommandHandler : IRequestHandler<BotLogCommand, ActionResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        internal static ISession _session;
        public BotLogCommandHandler(IUnitOfWork unitOfWork, ISession session)
        {
            _unitOfWork = unitOfWork;
            _session = session;
        }
        public async Task<ActionResult> Handle(BotLogCommand request, CancellationToken cancellationToken)
        {

            cancellationToken.ThrowIfCancellationRequested();

            _unitOfWork.BeginTransaction();
            var bot = _session.Load<BotEntity>(request.BotId);
            using (var trans = _session.BeginTransaction())
            {
                var botlog = new BotLogEntity
                {
                    Bot = bot,
                    Message = request.Message,
                    Type = request.Type,
                    CreationDateUTC = DateTime.Now
                };
                _session.Save(botlog);
                trans.Commit();
            }
            return new ActionResult { Suceeded = true };
        }
    }
}
