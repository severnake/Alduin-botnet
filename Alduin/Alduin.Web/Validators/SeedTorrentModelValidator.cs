using Alduin.Web.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alduin.Web.Validators
{
    public class SeedTorrentModelValidator : AbstractValidator<SeedTorrentModel>
    {
        public SeedTorrentModelValidator()
        {
            RuleFor(x => x.Url)
                .NotEmpty().WithMessage("Message is required!");
        }
    }
}
