using Alduin.Logic.Interfaces.Repositories;
using Alduin.Logic.Mediator.Queries;
using Alduin.Shared.DTOs;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Alduin.Logic.Mediator.Handlers.QueryHandlers
{
    public class GetBotListByStatusQueryHandler : IRequestHandler<GetBotsByStatusQuery, BotDTO[]>
    {
        private readonly IBotRepository _BotRepository;
        public GetBotListByStatusQueryHandler(IBotRepository BotRepository)
        {
            _BotRepository = BotRepository;
        }

        public Task<BotDTO[]> Handle(GetBotsByStatusQuery request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var Bot = _BotRepository.FindAddressByAvailable(request.status);

            return Task.FromResult(Bot);
        }
    }
}
