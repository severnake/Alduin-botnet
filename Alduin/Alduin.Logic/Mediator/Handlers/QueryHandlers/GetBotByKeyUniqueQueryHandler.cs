using Alduin.Logic.Interfaces.Repositories;
using Alduin.Logic.Mediator.Queries;
using Alduin.Shared.DTOs;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Alduin.Logic.Mediator.Handlers.QueryHandlers
{
    public class GetBotByKeyUniqueQueryHandler : IRequestHandler<GetBotByKeyUniqueQuery, BotDTO>
    {
        private readonly IBotRepository _botRepository;
        public GetBotByKeyUniqueQueryHandler(IBotRepository botRepository)
        {
            _botRepository = botRepository;
        }

        public Task<BotDTO> Handle(GetBotByKeyUniqueQuery request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var bot = _botRepository.FindBotByKeyUnique(request.KeyUnique);
            return Task.FromResult(bot);
        }
    }
}
