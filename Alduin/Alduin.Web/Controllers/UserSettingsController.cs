using Alduin.Logic.Identity;
using Alduin.Logic.Mediator.Commands;
using Alduin.Logic.Mediator.Queries;
using Alduin.Web.Models;
using Alduin.Web.Models.UserSettings;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading.Tasks;

namespace Alduin.Web.Controllers
{
    public class UserSettingsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IStringLocalizer<UserAccountController> _localizer;
        private readonly UserManager<AppIdentityUser> _userManager;
        public UserSettingsController(IMediator mediator, IStringLocalizer<UserAccountController> localizer, UserManager<AppIdentityUser> userManager)
        {
            _userManager = userManager;
            _mediator = mediator;
            _localizer = localizer;
        }
        [Authorize]
        public IActionResult Index()
        {
            return RedirectToAction(nameof(ChangePassword));
        }
        [Authorize]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            var title = _localizer["Change Password"];
            SetTitle(title);
            ViewData["Change Password"] = title;

            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            var title = _localizer["Change Password"];
            SetTitle(title);
            ViewData["Change Password"] = title;
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(User.Identity.Name);

            await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            return View(model);
        }
        [Authorize]
        public IActionResult UsersSettings()
        {
            if (User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value == "Admin")
            {
                return View();
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
        [Authorize]
        public async Task<IActionResult> UsersRoleList()
        {
            if (User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value == "Admin")
            {
                var query = new GetAllRoleQuery();
                var bot = await _mediator.Send(query);
                return Json(bot);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
        [HttpPost]
        [Produces("application/json")]
        [Route("ChangeRole")]
        public async Task<JsonResult> UsersSettingsAsync([FromBody] RoleChangeModel model)
        {
            if (User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value != "Admin")
            {
                return Json(false);
            }
            var query = new ChangeClaimCommand
            {
                UserId = model.UserId,
                ClaimValue = model.Role
            };
            var resultClaim = await _mediator.Send(query);
            if (resultClaim.Suceeded)
            {
                return Json(true);
            }
            else{
                return Json(false);
            }
        }
    }
}