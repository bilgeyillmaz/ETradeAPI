using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Email).NotNull().WithMessage("Email address is required.").NotEmpty()
                .WithMessage("Email address is required.");
            RuleFor(x => x.Password).NotNull().WithMessage("Password address is required.").NotEmpty()
                .WithMessage("Password address is required.");
        }
    }
}
