using Alduin.Web.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alduin.Web.Validators
{
    public class ExecuteCommandValidator : AbstractValidator<ExecuteModel>
    {
        public ExecuteCommandValidator()
        {
            RuleFor(x => x.Url)
                .NotEmpty().WithMessage("Url is required!");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("File name is required!");
        }
    }
}
