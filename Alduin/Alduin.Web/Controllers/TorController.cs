using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alduin.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Alduin.Server.Modules;
using System.Threading;
using Microsoft.AspNetCore.Authorization;

namespace Alduin.Web.Controllers
{
    public class TorController : ControllerBase
    {
        [Authorize]
        public IActionResult Index()
        {
            var OnionAddress = "";
            try
            {
               OnionAddress = ServerFileManager.FileReader(ConfigTor.TorBaseFolder + @"/hostname");
            }
            catch
            {
                Thread thr = new Thread(new ThreadStart(ConfigTor.StartTor));
                thr.Start();
                OnionAddress = "Read error! Please reload page!";
            };
            
            ViewData["OnionAddress"] = OnionAddress.Replace("\r\n", "");
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
            var OnionAddress = "";
            try
            {
                OnionAddress = ServerFileManager.FileReader(ConfigTor.TorBaseFolder + @"/hostname");
            }
            catch
            {
                OnionAddress = "Read error! Please reload page!";
            };
            Thread.Sleep(5000);
            return Content(OnionAddress);
        }
        [Authorize]
        [HttpPost]
        public IActionResult EditTorch(EditTorchFileModel model)
        {
            ServerFileManager.FileWriter(ConfigTor.TorrcPath, model.Torch);
            return View(model);
        }
    }
}