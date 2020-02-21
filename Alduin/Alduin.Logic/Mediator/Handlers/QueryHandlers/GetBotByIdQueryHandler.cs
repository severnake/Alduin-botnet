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
    public class GetBotByIdQueryHandler : IRequestHandler<GetBotByIdQuery, BotDTO>
    {
        private readonly IBotRepository _botRepository;
        public GetBotByIdQueryHandler(IBotRepository botRepository)
        {
            _botRepository = botRepository;
        }

        public Task<BotDTO> Handle(GetBotByIdQuery request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var bot = _botRepository.Get(request.Id);
            return Task.FromResult(bot);
        }
    }
}
