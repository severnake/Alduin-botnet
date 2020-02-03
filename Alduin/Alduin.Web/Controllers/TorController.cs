using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alduin.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Alduin.Server.Modules;
using AutoMapper.Configuration;
using Alduin.Server.Models;

namespace Alduin.Web.Controllers
{
    public class TorController : ControllerBase
    {
        public IActionResult Index()
        {
            var OnionAddress = ServerFile.FileReader(ConfigTor.TorBaseFolder + @"/hostname");
            ViewData["OnionAddress"] = OnionAddress.Replace("\r\n", "");
            return View();
        }
        public IActionResult EditTorch()
        {
            var model = new EditTorchFileModel
            {
                Torch = ServerFile.FileReader(ConfigTor.TorrcPath)
            };
            return View(model);
        }
        public IActionResult Logger()
        {
            var log = TorLogModel.Log;
            return Content(log);
        }
        [HttpPost]
        public IActionResult EditTorch(EditTorchFileModel model)
        {
            ServerFile.FileWriter(ConfigTor.TorrcPath, model.Torch);
            return View(model);
        }
    }
}