using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alduin.Server.Commands;
using Alduin.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Alduin.Web.Controllers
{
    public class CommandsController : ControllerBase
    {
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
        public IActionResult SetCommand(ExecuteModel model)
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
                Force = model.Force
            };

            return Json(true);
        }
        [Authorize]
        [HttpPost]
        public IActionResult SetCommand(WebsiteModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            return Json(true);
        }
    }
}