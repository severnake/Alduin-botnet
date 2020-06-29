using Alduin.Web.Models;
using FluentValidation;

namespace Alduin.Web.Validators
{
    public class CMDModelValidator : AbstractValidator<CMDModel>
    {
        public CMDModelValidator()
        {
            RuleFor(x => x.command)
                .NotEmpty().WithMessage("Message is required!");
        }
    }
}
