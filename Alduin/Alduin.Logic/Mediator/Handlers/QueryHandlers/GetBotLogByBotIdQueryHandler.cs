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
    public class GetBotLogByBotIdQueryHandler : IRequestHandler<GetBotLogByBotIdQuery, BotLogDTO[]>
    {
        private IBotLogRepository _botLogRepository;
        public GetBotLogByBotIdQueryHandler(IBotLogRepository botLogRepository)
        {
            _botLogRepository = botLogRepository;
        }

        public Task<BotLogDTO[]> Handle(GetBotLogByBotIdQuery request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = _botLogRepository.FindByBotId(request.BotId);

            return Task.FromResult(result);
        }
    }
}
