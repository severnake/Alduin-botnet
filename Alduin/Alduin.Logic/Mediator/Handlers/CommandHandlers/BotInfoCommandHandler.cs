using Alduin.DataAccess.Entities;
using Alduin.Logic.DTOs;
using Alduin.Logic.Mediator.Commands;
using Alduin.Shared.Interfaces.UnitOfWork;
using MediatR;
using NHibernate;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Alduin.Logic.Mediator.Handlers.CommandHandlers
{
    public class BotInfoCommandHandler : IRequestHandler<BotInfoCommand, ActionResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        internal static ISession _session;
        public BotInfoCommandHandler(IUnitOfWork unitOfWork, ISession session)
        {
            _unitOfWork = unitOfWork;
            _session = session;
        }

        public async Task<ActionResult> Handle(BotInfoCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            _unitOfWork.BeginTransaction();
            var bot = _session.Load<BotEntity>(request.BotId);
            using (var trans = _session.BeginTransaction())
            {
                var botentity = new BotInfoEntity
                {
                    Bot = bot,
                    HardwareName = request.HardwareName,
                    HardwareType = request.HardwareType,
                    OtherInformation = request.OtherInformation,
                    Performance = request.Performance,
                    CreationDateUTC = DateTime.Now
                };
                _session.Save(botentity);
                trans.Commit();
            }
            return new ActionResult { Suceeded = true };
        }
    }
}
