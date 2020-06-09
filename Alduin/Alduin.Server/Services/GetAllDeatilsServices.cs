using Alduin.Logic.Mediator.Queries;
using Alduin.Server.Commands;
using Alduin.Server.Handler;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Alduin.Server.Services
{
    public class GetAllDeatilsServices
    {
        private readonly IMediator _mediator;

        public GetAllDeatilsServices(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<string> GetAllDeatils(int botid)
        {
            var method = new BaseCommands
            {
                Method = "GetAllDetails"
            };
            var bot = new GetBotByIdQuery
            {
                Id = botid
            };
            var botlist = await _mediator.Send(bot);
            var response = CommandExecute.TcpConnects(botlist, JsonConvert.SerializeObject(method));
            return response;
        }
    }
}
