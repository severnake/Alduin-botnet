using Alduin.Shared.DTOs;
using MediatR;


namespace Alduin.Logic.Mediator.Queries
{
    public class GetBotsByStatusQuery : IRequest<BotDTO[]>
    {
        public bool status { get; set; }
    }
}
