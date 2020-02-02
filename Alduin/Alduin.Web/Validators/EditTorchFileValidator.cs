using Alduin.Web.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alduin.Web.Validators
{
    public class EditTorchFileValidator : AbstractValidator<EditTorchFileModel>
    {
        public EditTorchFileValidator()
        {
            RuleFor(x => x.Torch)
                .NotEmpty().WithMessage("Torch is required!");
        }
    }
}
