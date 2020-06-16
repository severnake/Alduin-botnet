using Alduin.Commands;
using Alduin.Logic.Mediator.Commands;
using Alduin.Shared.DTOs;
using MediatR;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Alduin.Server.Services
{
    public class UpdateBotDeatilsService
    {
        private readonly IMediator _mediator;

        public UpdateBotDeatilsService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Update(string botDeatils, int id)
        {
            HardwareCollectorCommand ConvertedBotDeatils = JsonConvert.DeserializeAnonymousType(botDeatils, new HardwareCollectorCommand());
            var cpu = new BotInfoCommand
            {
                BotId = id,
                HardwareName = ConvertedBotDeatils.Cpu.HardwareName,
                HardwareType = ConvertedBotDeatils.Cpu.HardwareType,
                Performance = ConvertedBotDeatils.Cpu.Performance,
                OtherInformation = ConvertedBotDeatils.Cpu.OtherInformation
            };
            await _mediator.Send(cpu);
            var gpu = new BotInfoCommand
            {
                BotId = id,
                HardwareName = ConvertedBotDeatils.Gpu.HardwareName,
                HardwareType = ConvertedBotDeatils.Gpu.HardwareType,
                Performance = ConvertedBotDeatils.Gpu.Performance,
                OtherInformation = ConvertedBotDeatils.Gpu.OtherInformation
            };
            await _mediator.Send(gpu);
            var os = new BotInfoCommand
            {
                BotId = id,
                HardwareName = ConvertedBotDeatils.Os.HardwareName,
                HardwareType = ConvertedBotDeatils.Os.HardwareType,
                Performance = ConvertedBotDeatils.Os.Performance,
                OtherInformation = ConvertedBotDeatils.Os.OtherInformation
            };
            await _mediator.Send(os);
            var ram = new BotInfoCommand
            {
                BotId = id,
                HardwareName = ConvertedBotDeatils.Ram.HardwareName,
                HardwareType = ConvertedBotDeatils.Ram.HardwareType,
                Performance = ConvertedBotDeatils.Ram.Performance,
                OtherInformation = ConvertedBotDeatils.Ram.OtherInformation
            };
            await _mediator.Send(ram);
            for (var i = 0; i < ConvertedBotDeatils.OtherHarwares[0].Count; i++)
            {
                var command = new BotInfoCommand
                {
                    BotId = id,
                    HardwareName = ConvertedBotDeatils.OtherHarwares[0][i].HardwareName,
                    HardwareType = ConvertedBotDeatils.OtherHarwares[0][i].HardwareType,
                    Performance = ConvertedBotDeatils.OtherHarwares[0][i].Performance,
                    OtherInformation = ConvertedBotDeatils.OtherHarwares[0][i].OtherInformation
                };
                await _mediator.Send(command);
            }
        }

    }
}
