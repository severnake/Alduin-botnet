using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alduin.Logic.Mediator.Queries;
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
    }
}