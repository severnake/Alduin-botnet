using Alduin.Logic.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alduin.Logic.Mediator.Commands
{
    public class BotLogCommand : IRequest<ActionResult>
    {
        public int? BotId { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
    }
}
