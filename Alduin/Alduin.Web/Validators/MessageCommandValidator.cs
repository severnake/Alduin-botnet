using Alduin.Web.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alduin.Web.Validators
{
    public class MessageCommandValidator : AbstractValidator<MessageModel>
    {
        public MessageCommandValidator()
        {
            RuleFor(x => x.Msg)
                .NotEmpty().WithMessage("Message is required!");
        }
    }
}
