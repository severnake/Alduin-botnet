using Alduin.Web.Models;
using FluentValidation;

namespace Alduin.Web.Validators
{
    public class AdsModelValidator : AbstractValidator<AdsModel>
    {
        public AdsModelValidator()
        {
            RuleFor(x => x.Url)
                .NotEmpty().WithMessage("Message is required!");
        }
    }
}
