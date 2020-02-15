using Alduin.Logic.DTOs;
using MediatR;

namespace Alduin.Server.Commands
{
    public class BaseCommands : IRequest<ActionResult>
    {
        public string Method { get; set; }
        public bool Force { get; set; }
    }
}
