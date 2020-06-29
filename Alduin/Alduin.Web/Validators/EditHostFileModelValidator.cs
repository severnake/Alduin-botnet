using Alduin.Web.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alduin.Web.Validators
{
    public class EditHostFileModelValidator : AbstractValidator<EditHostFileModel>
    {
        public EditHostFileModelValidator()
        {
            RuleFor(x => x.Line)
                .NotEmpty().WithMessage("Message is required!");
        }
    }
}
