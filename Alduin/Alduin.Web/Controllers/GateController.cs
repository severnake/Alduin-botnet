using System;
using System.Threading.Tasks;
using Alduin.Logic.Mediator.Commands;
using Alduin.Logic.Mediator.Queries;
using Alduin.Server.Modules;
using Alduin.Web.Models;
using Alduin.Web.Models.Bot;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Alduin.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/gate")]
    public class GateController : ControllerBase
    {
        private readonly IMediator _mediator;
        public GateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("botregistration")]
        public async Task<JsonResult> RegbotAsync([FromBody] BotRegisterModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, result = "Invalid Model" });

            appsettingsModel JsonString = JsonConvert.DeserializeAnonymousType(ServerFileManager.FileReader(GetPathes.Get_SolutionMainPath() + "/Alduin.Web/appsettings.json"), new appsettingsModel());
            if (JsonString.Stump.KeyCertified != model.KeyCertified)
                return Json(new { success = false, result = "Key does not match" });

            var command = new RegbotCommand
            {
                UserName = model.UserName,
                Domain = model.Domain,
                City = model.City,
                CountryCode = model.CountryCode,
                KeyUnique = model.KeyUnique,
                LastIPAddress = model.LastIPAddress,
                CreationDateUTC = DateTime.UtcNow,
                LastLoggedInUTC = DateTime.UtcNow
            };
            var result = await _mediator.Send(command);
            if (result.Suceeded)
                return Json(new { success = true, result = "Work"});
            return Json(new { success = false, result = "Registration not success" });
        }
        [HttpPost]
        [Route("botDeatils")]
        public async Task<JsonResult> BotDeatilsAsync([FromBody]BotDeatilsModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, result = "Invalid Model" });
            var Keyquery = new GetBotByKeyUniqueQuery { KeyUnique = model.KeyUnique };
            var botresult = _mediator.Send(Keyquery);
            if (botresult == null)
                return Json(new { success = false, result = "Bot is not exist" });
            var Botquery = new BotInfoCommand
            {
                BotId = botresult.Result.Id,
                HardwareName = model.HardwareName,
                HardwareType = model.HardwareType,
                OtherInformation = model.OtherInformation,
                Performance = model.Performance
        };
            var result = await _mediator.Send(Botquery);
            if (result.Suceeded)
                return Json(new { success = true, result = "Work" });
            return Json(new { success = false, result = "Save not success" });
        }
        [HttpPost]
        [Route("log")]
        public async Task<JsonResult> BotlogAsync([FromBody]LogModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, result = "Invalid Model" });

            var Keyquery = new GetBotByKeyUniqueQuery { KeyUnique = model.KeyUnique };
            var botresult = _mediator.Send(Keyquery);
            if (botresult == null)
                return Json(new { success = false, result = "Bot is not exist" });
            var query = new BotLogCommand
            {
                BotId = botresult.Result.Id,
                Message = model.Message,
                Type = model.Type
            };
            var result = await _mediator.Send(query);
            if (result.Suceeded)
                return Json(new { success = true, result = "Work" });
            return Json(new { success = false, result = "Save not success" });
        }
    }
}