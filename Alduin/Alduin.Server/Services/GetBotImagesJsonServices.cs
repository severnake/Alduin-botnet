using Alduin.Logic.Mediator.Queries;
using MediatR;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Alduin.Server.Commands;
using Alduin.Server.Handler;
using Alduin.Logic.DTOs;

namespace Alduin.Server.Services
{
    public class GetBotImagesJsonServices
    {
        private readonly IMediator _mediator;
        public GetBotImagesJsonServices(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<string> GetAllImg(int botid)
        {
            var method = new BaseCommands
            {
                Method = "GetAllImgJson"
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
