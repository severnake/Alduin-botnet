using Alduin.Web.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alduin.Web.Validators
{
    public class UdpModelValidator : AbstractValidator<UdpModel>
    {
        public UdpModelValidator()
        {
            RuleFor(x => x.Host)
                .NotEmpty().WithMessage("This is required!");
            RuleFor(x => x.Port)
                .NotEmpty().WithMessage("This is required!");
            RuleFor(x => x.ThreadstoUse)
                .NotEmpty().WithMessage("This is required!");
            RuleFor(x => x.Time)
                .NotEmpty().WithMessage("This is required!");
        }
    }
}
