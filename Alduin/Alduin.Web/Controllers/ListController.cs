using System;
using System.Threading.Tasks;
using Alduin.Logic.Mediator.Queries;
using Alduin.Web.Models;
using Alduin.Web.Models.Bot;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Alduin.Web.Controllers
{
    public class ListController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IStringLocalizer<ListController> _localizer;
        public ListController(IMediator mediator, IStringLocalizer<ListController> localizer)
        {
            _mediator = mediator;
            _localizer = localizer;
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
                var query = new GetBotByIdQuery
                {
                    Id = id
                };
                var bot = await _mediator.Send(query);
                DateTime DateNowUTC = DateTime.UtcNow.AddMinutes(-5);
                var status = _localizer["Offline"];
                if (bot.LastLoggedInUTC >= DateNowUTC)
                    status = _localizer["Online"];
                var botDeatils = new BotDeatilsInquiryModel
                {
                    Name = bot.UserName,
                    Domain = bot.Domain,
                    LastIPAddress = bot.LastIPAddress,
                    LastLoggedInUTC = bot.LastLoggedInUTC,
                    Status = status
                };
                return View(botDeatils);
            }
            catch
            {
                return RedirectToAction(nameof(List));
            };
        }
    }
}