using System;
using System.Threading.Tasks;
using Alduin.Logic.Mediator.Queries;
using Alduin.Server.Modules;
using Alduin.Server.Services;
using Alduin.Web.Models;
using Alduin.Web.Models.Commands.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;

namespace Alduin.Web.Controllers
{
    public class ListController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IStringLocalizer<ListController> _localizer;
        private readonly GetBotImagesJsonServices _getBotImagesJsonServices;
        public ListController(IMediator mediator, IStringLocalizer<ListController> localizer, GetBotImagesJsonServices getBotImagesJsonServices)
        {
            _mediator = mediator;
            _localizer = localizer;
            _getBotImagesJsonServices = getBotImagesJsonServices;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult List()
        {
            return View();
        }
        [Authorize]
        public async Task<IActionResult> BotList()
        {
            var query = new GetBotListQuery();
            var bot = await _mediator.Send(query);
            return Json(bot);
        }
        [Authorize]
        public async Task<IActionResult> Bot(int id)
        {
            try
            {
                appsettingsModel appsettings = JsonConvert.DeserializeAnonymousType(ServerFileManager.FileReader(GetPathes.Get_SolutionMainPath() + "/Alduin.Web/appsettings.json"), new appsettingsModel());
                var query = new GetBotByIdQuery
                {
                    Id = id
                };
                GetImgJsonModel ImagesJsonModel = JsonConvert.DeserializeAnonymousType(await _getBotImagesJsonServices.GetAllImg(id), new GetImgJsonModel());
                var bot = await _mediator.Send(query);
                DateTime DateNowUTC = DateTime.UtcNow.AddMinutes(-5);
                var status = _localizer["Offline"];
                if (bot.LastLoggedInUTC >= DateNowUTC)
                    status = _localizer["Online"];
                var botInquiryDeatils = new BotDeatilsInquiryModel
                {
                    Name = bot.UserName,
                    Domain = bot.Domain,
                    LastIPAddress = bot.LastIPAddress,
                    LastLoggedInUTC = bot.LastLoggedInUTC,
                    Status = status,
                    KeyCertified = bot.KeyCertified,
                    KeyUnique = appsettings.Stump.KeyCertified
                };
                var botmodel = new BotModel
                {
                    newImagesJsonModel = ImagesJsonModel,
                    newBotDeatilsInquiryModel = botInquiryDeatils
                };
                return View(botmodel);
            }
            catch
            {
                return RedirectToAction(nameof(List));
            };
        }
        [Authorize]
        public async Task<IActionResult> Stream()
        {
            var bots = new GetBotsByStatusQuery
            {
                status = false //offline
            };
            var botlist = await _mediator.Send(bots);
            var model = new StreamModel();
            for (int i = 0; i < botlist.Length; i++)
            {
                model.NewVariables[i].Domain = botlist[i].Domain;
                model.NewVariables[i].Name = botlist[i].UserName;
            }
            return View(model);
        }
    }
}