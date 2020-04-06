using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alduin.Logic.Mediator.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Alduin.Logic.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Alduin.Logic.Mediator.Queries;
using Microsoft.AspNetCore.Authorization;

namespace Alduin.Web.Controllers
{
    public class InvitationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IStringLocalizer<InvitationController> _localizer;
        private static Random random = new Random();
        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly string microsoftPage = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
        public InvitationController(IMediator mediator, IStringLocalizer<InvitationController> localizer, UserManager<AppIdentityUser> userManager)
        {
            _userManager = userManager;
            _mediator = mediator;
            _localizer = localizer;
        }
        [Authorize]
        public IActionResult Index()
        {
            if (User.Claims.FirstOrDefault(c => c.Type == microsoftPage).Value != "User")
            {
                return View();
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
        [Authorize]
        public async Task<IActionResult> GenerateNew()
        {
            if (User.Claims.FirstOrDefault(c => c.Type == microsoftPage).Value == "User")
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            var key = RandomString(10);
            var InvitationCommand = new RegInvitationCommand
            {
                invitationKey = key,
                Used = false,
                UserId = user.Id
            };
            var result = await _mediator.Send(InvitationCommand);
            if (result.Suceeded)
            {
                return Content(_localizer["Invitation generate success"]);
            }
            else
            {
                return Content(_localizer["Invitation generate unsuccess"]);
            }
        }
        [Authorize]
        public async Task<IActionResult> GetUserInvitation()
        {
            if (User.Claims.FirstOrDefault(c => c.Type == microsoftPage).Value == "User")
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            var query = new GetInvitationByUserQuery { UserId = user.Id };
            var result = await _mediator.Send(query);
            return Json(result);
        }
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
    
}