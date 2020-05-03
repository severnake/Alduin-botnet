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
        public IActionResult EditTorch()
        {
            if (User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value == "User")
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
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
            if (User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value == "User")
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            ServerFileManager.FileWriter(GetPathes.Get_LogPath() + @"\Log.txt", "");
            return Content("Ok");
        }
        [Authorize]
        public IActionResult GenerateNewAddress()
        {
            if (User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value == "User")
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
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
            if (User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value == "User")
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            ServerFileManager.FileWriter(ConfigTor.TorrcPath, model.Torch);
            return View(model);
        }
        [Authorize]
        public IActionResult RestartTor()
        {
            if (User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value == "User")
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            ConfigTor.KillTor();
            ConfigTor.StartTor();
            return Content("Ok");
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