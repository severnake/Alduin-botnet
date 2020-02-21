using System;
using System.Threading.Tasks;
using Alduin.Logic.Mediator.Queries;
using Alduin.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Alduin.Web.Controllers
{
    public class ListController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ListController(IMediator mediator)
        {
            _mediator = mediator;
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
            if(id == null)
                return RedirectToAction(nameof(List));

            var query = new GetBotByIdQuery
            {
                Id = id
            };
            var bot = await _mediator.Send(query);
            DateTime DateNowUTC = DateTime.UtcNow;
            DateNowUTC = DateNowUTC.AddMinutes(-5);
            var status = "Offline";
            if (bot.LastLoggedInUTC >= DateNowUTC)
                status = "Online";
            var botDeatils = new BotDeatilsModel
            {
                Name = bot.UserName,
                Domain = bot.Domain,
                LastIPAddress = bot.LastIPAddress,
                LastLoggedInUTC = bot.LastLoggedInUTC,
                Status = status
            };
            return View(botDeatils);
        }
    }
}