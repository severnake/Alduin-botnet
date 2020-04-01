using Alduin.Logic.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alduin.Logic.Mediator.Commands
{
    public class ChangeClaimCommand : IRequest<ActionResult>
    {
        public int UserId { get; set; }
        public string ClaimValue { get; set; }
    }
}
