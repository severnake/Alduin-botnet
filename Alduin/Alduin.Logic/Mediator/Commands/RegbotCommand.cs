using Alduin.Logic.DTOs;
using MediatR;
using System;

namespace Alduin.Logic.Mediator.Commands
{
    public class RegbotCommand : IRequest<ActionResult>
    {
        public string UserName { get; set; }
        public string KeyUnique { get; set; }
        public string KeyCertified { get; set; }
        public string Domain { get; set; }
        public string CountryCode { get; set; }
        public string LastIPAddress { get; set; }
        public string City { get; set; }
        public DateTime CreationDateUTC { get; set; }
        public DateTime? LastLoggedInUTC { get; set; }
    }
}
