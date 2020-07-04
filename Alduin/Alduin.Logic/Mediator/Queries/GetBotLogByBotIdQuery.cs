using Alduin.Shared.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alduin.Logic.Mediator.Queries
{
    public class GetBotLogByBotIdQuery : IRequest<BotLogDTO[]>
    {
        public int BotId { get; set; }
    }
}
