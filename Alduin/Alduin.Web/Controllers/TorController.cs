using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alduin.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Alduin.Server.Modules;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;
using System.Diagnostics;

namespace Alduin.Web.Controllers
{
    public class TorController : ControllerBase
    {
        private readonly IStringLocalizer<HomeController> _localizer;

        public TorController(IStringLocalizer<HomeController> localizer)
        {
            _localizer = localizer;
        }

        [Authorize]
        public IActionResult Index()
        {
            ViewData["OnionAddress"] = readTorch().Replace("\r\n", "");
            return View();
        }
        [Authorize]
        public IActionResult EditTorch()
        {
            var model = new EditTorchFileModel
            {
                Torch = ServerFileManager.FileReader(ConfigTor.TorrcPath)
            };
            return View(model);
        }
        [Authorize]
        public IActionResult Logger()
        {
            var log = ServerFileManager.FileReader(GetPathes.Get_LogPath() + @"\Log.txt");
            return Content(log);
        }
        [Authorize]
        public IActionResult DeleteLog()
        {
            ServerFileManager.FileWriter(GetPathes.Get_LogPath() + @"\Log.txt", "");
            return Content("Ok");
        }
        [Authorize]
        public IActionResult GenerateNewAddress()
        {
            try
            {
                ServerFileManager.FileDelete(ConfigTor.TorBaseFolder + @"\hostname");
                ServerFileManager.FileDelete(ConfigTor.TorBaseFolder + @"\private_key");
            }
            catch {};
            Thread thr = new Thread(new ThreadStart(ConfigTor.StartTor));
            thr.Start();
            Thread.Sleep(5000);
            return Content(readTorch());
        }
        [Authorize]
        [HttpPost]
        public IActionResult EditTorch(EditTorchFileModel model)
        {
            ServerFileManager.FileWriter(ConfigTor.TorrcPath, model.Torch);
            return View(model);
        }
        private string readTorch()
        {
            var OnionAddress = "";
            try
            {
                OnionAddress = ServerFileManager.FileReader(ConfigTor.TorBaseFolder + @"/hostname");
            }
            catch
            {
                Process[] p;
                p = Process.GetProcessesByName(ConfigTor.Tor);
                if (!(p.Length > 0))
                {
                    Thread thr = new Thread(new ThreadStart(ConfigTor.StartTor));
                    thr.Start();
                }
                OnionAddress = _localizer["Read error! Please reload page!"];
            };
            return OnionAddress;
        } 
    }
}