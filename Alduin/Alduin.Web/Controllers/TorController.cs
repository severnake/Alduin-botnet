using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alduin.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Alduin.Server.Modules;
namespace Alduin.Web.Controllers
{
    public class TorController : Controller
    {
        public IActionResult Index()
        {
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
        [HttpPost]
        public IActionResult EditTorch(EditTorchFileModel model)
        {
            ServerFile.FileWriter(ConfigTor.TorrcPath, model.Torch);
            return View(model);
        }
    }
}