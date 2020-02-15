using Alduin.Logic.DTOs;
using Alduin.Logic.Mediator.Commands;
using Alduin.Logic.Mediator.Queries;
using Alduin.Shared.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Alduin.Logic.Services
{
    public class RegisterService
    {
        private readonly IMediator _mediator;

        public RegisterService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ActionResult> Register(UserDTO user, string password, string invitationKey)
        {
            var result = new ActionResult();
            var query = new GetInvitationByKeyQuery { invitationKey = invitationKey };
            var resultKey = await _mediator.Send(query);
            if (resultKey == null || resultKey.Used)
            {
                result.Suceeded = false;
                result.ErrorMessages.Add("Invalid invitation key!");
                return result;
            }

            var registerCommand = new RegisterCommand
            {
                User = user,
                Password = password
            };
            var UpdateInvitationCommand = new UpdateInvitationCommand
            {
                id = resultKey.Id,
            };
            var RegisterResult = await _mediator.Send(registerCommand);
            if (RegisterResult.Suceeded)
            {
                var confirm = await _mediator.Send(UpdateInvitationCommand);
                result.Suceeded = true;
            }
            else
            {
                foreach (var msg in result.ErrorMessages)
                    result.ErrorMessages.Add(msg);
            }
            return result;
        }
    }
}
