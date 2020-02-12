using Alduin.Web.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alduin.Web.Validators
{
    public class OpenWebsiteValidator : AbstractValidator<WebsiteModel>
    {
        public OpenWebsiteValidator()
        {
            RuleFor(x => x.Url)
                .NotEmpty().WithMessage("Torch is required!");
        }
    }
}
