using Alduin.Shared.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alduin.Logic.Mediator.Queries
{
    public class GetAllUsersQuery : IRequest<UserDTO[]>
    {
    }
}
