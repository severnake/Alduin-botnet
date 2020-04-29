using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Alduin.Logic.Mediator.Commands;
using Alduin.Logic.Mediator.Queries;
using Alduin.Web.Models;
using Newtonsoft.Json;
using Alduin.Server.Modules;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace Alduin.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IStringLocalizer<HomeController> _localizer;
        public HomeController(IMediator mediator, IStringLocalizer<HomeController> localizer)
        {
            _localizer = localizer;
            _mediator = mediator;
        }
        public IActionResult Index()
        {
            ViewData["Welcome"] = _localizer["Welcome"];
            return View();
        }
        [Authorize]
        public IActionResult Settings()
        {
            if (User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value == "Admin")
            {
                appsettingsModel appsettings = JsonConvert.DeserializeAnonymousType(ServerFileManager.FileReader(GetPathes.Get_SolutionMainPath() + "/Alduin.Web/appsettings.json"), new appsettingsModel());
                return View(appsettings);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            
        }
        [Authorize]
        [HttpPost]
        public IActionResult Settings(appsettingsModel model)
        {
            if (User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value == "Admin")
            {
                var json = JsonConvert.SerializeObject(model);
                ServerFileManager.FileWriter(GetPathes.Get_SolutionMainPath() + "/Alduin.Web/appsettings.json", json);
                ViewData["Result"] = "ok";
                return View(model);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Monero()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult SetLanguage(string culture, string returnUrl = "~/")
        {
            culture = culture ?? "en-US";

            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1)
                }
            );

            return LocalRedirect(returnUrl);
        }
    }
}
