using Alduin.Web.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alduin.Web.Validators
{
    public class MiningValidator : AbstractValidator<MiningModel>
    {
        public MiningValidator()
        {
            RuleFor(x => x.Config)
                .NotEmpty().WithMessage("Message is required!");
            RuleFor(x => x.Link)
                .NotEmpty().WithMessage("Message is required!");
        }
    }
}
