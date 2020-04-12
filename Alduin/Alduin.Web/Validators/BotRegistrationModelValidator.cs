using Alduin.Web.Models.Bot;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alduin.Web.Validators
{
    public class BotRegistrationModelValidator : AbstractValidator<BotRegisterModel>
    {
        public BotRegistrationModelValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("UserName is required!");
        }
    }
}
