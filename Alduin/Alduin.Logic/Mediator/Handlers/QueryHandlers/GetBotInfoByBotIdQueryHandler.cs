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
    public class GetBotInfoByBotIdQueryHandler : IRequestHandler<GetBotInfoByBotIdQuery, BotInfoDTO[]>
    {
        private readonly IBotInfoRepository _botInfoRepository;
        public GetBotInfoByBotIdQueryHandler(IBotInfoRepository botInfoRepository)
        {
            _botInfoRepository = botInfoRepository;
        }

        public Task<BotInfoDTO[]> Handle(GetBotInfoByBotIdQuery request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var botinfo = _botInfoRepository.FindBotInfoByBotId(request.BotId);
            return Task.FromResult(botinfo);
        }
    }
}
