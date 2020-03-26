using System.Threading.Tasks;
using Alduin.Web.Models.Bot;
using Microsoft.AspNetCore.Mvc;

namespace Alduin.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/gate")]
    public class GateController : ControllerBase
    {
        [HttpPost]
        [Route("botregistration")]
        public JsonResult Regbot([FromBody]BotRegisterModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, result = "" });

            return Json(new { success = true, result = "Work"});
        }
        [HttpPost]
        [Route("botDeatils")]
        public JsonResult BotDeatils([FromBody]BotDeatilsModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, result = "" });
            //return NotFound();

            return Json(new { success = true, result = "Work" });
        }
    }
}