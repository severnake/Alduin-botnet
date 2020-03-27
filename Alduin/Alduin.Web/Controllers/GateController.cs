using System;
using System.Threading.Tasks;
using Alduin.Logic.Mediator.Commands;
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
        public async Task<JsonResult> RegbotAsync([FromBody]BotRegisterModel model)
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
            return Json(new { success = true, result = "Work"});
        }
        [HttpPost]
        [Route("botDeatils")]
        public JsonResult BotDeatils([FromBody]BotDeatilsModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, result = "" });

            return Json(new { success = true, result = "Work" });
        }
        [HttpPost]
        [Route("log")]
        public JsonResult Botlog([FromBody]LogModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, result = "" });

            return Json(new { success = true, result = "Work" });
        }
    }
}