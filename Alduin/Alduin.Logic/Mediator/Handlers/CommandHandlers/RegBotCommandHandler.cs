using Alduin.DataAccess.Entities;
using Alduin.Logic.DTOs;
using Alduin.Logic.Mediator.Commands;
using Alduin.Shared.Interfaces.UnitOfWork;
using AutoMapper;
using MediatR;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Alduin.Logic.Mediator.Handlers.CommandHandlers
{
    public class RegBotCommandHandler : IRequestHandler<RegbotCommand, ActionResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        internal static ISession _session;
        public RegBotCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, ISession session)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _session = session;
        }

        public async Task<ActionResult> Handle(RegbotCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            _unitOfWork.BeginTransaction();
            using (var trans = _session.BeginTransaction())
            {
                var Regbot = new BotEntity
                {
                    UserName = request.UserName,
                    Domain = request.Domain,
                    CountryCode = request.CountryCode,
                    KeyUnique = request.KeyUnique,
                    City = request.City,
                    CreationDateUTC = request.CreationDateUTC,
                    LastIPAddress = request.LastIPAddress,
                    LastLoggedInUTC = request.LastLoggedInUTC
                };
                _session.Save(Regbot);
                trans.Commit();
            }

            return new ActionResult { Suceeded = true };
        }
    }
}
