using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alduin.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Alduin.Server.Modules;


namespace Alduin.Web.Controllers
{
    public class TorController : ControllerBase
    {
        public IActionResult Index()
        {
            var OnionAddress = ServerFileManager.FileReader(ConfigTor.TorBaseFolder + @"/hostname");
            ViewData["OnionAddress"] = OnionAddress.Replace("\r\n", "");
            return View();
        }
        public IActionResult EditTorch()
        {
            var model = new EditTorchFileModel
            {
                Torch = ServerFileManager.FileReader(ConfigTor.TorrcPath)
            };
            return View(model);
        }
        public IActionResult Logger()
        {
            var log = ServerFileManager.FileReader(GetPathes.Get_LogPath() + @"\Log.txt");
            return Content(log);
        }
        public IActionResult DeleteLog()
        {
            ServerFileManager.FileWriter(GetPathes.Get_LogPath() + @"\Log.txt", "");
            return Content("Ok");
        }
        public IActionResult GenerateNewAddress()
        {
            ServerFileManager.FileDelete(ConfigTor.TorBaseFolder + @"\hostname");
            ServerFileManager.FileDelete(ConfigTor.TorBaseFolder + @"\private_key");
            ConfigTor.StartTor();
            return RedirectToAction(nameof(TorController.Index), "Index");
        }
        [HttpPost]
        public IActionResult EditTorch(EditTorchFileModel model)
        {
            ServerFileManager.FileWriter(ConfigTor.TorrcPath, model.Torch);
            return View(model);
        }
    }
}