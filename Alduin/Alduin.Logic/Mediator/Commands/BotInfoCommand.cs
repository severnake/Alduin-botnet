using Alduin.Logic.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alduin.Logic.Mediator.Commands
{
    public class BotInfoCommand : IRequest<ActionResult>
    {
        public int? BotId { get; set; }
        public string HardwareName { get; set; }
        public string HardwareType { get; set; }
        public string Performance { get; set; }
        public string OtherInformation { get; set; }
    }
}
