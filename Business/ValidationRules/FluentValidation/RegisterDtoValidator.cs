using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.Username).NotNull().WithMessage("Username is required. is required.").NotEmpty()
                .WithMessage("Username is required is required.");
            RuleFor(x => x.EmailAddress).NotNull().WithMessage("Email address is required.").NotEmpty()
                .WithMessage("Email address is required.");
            RuleFor(x => x.Password).NotNull().WithMessage("Password address is required.").NotEmpty()
                .WithMessage("Password address is required.");
            RuleFor(x => x.PhoneNumber).NotNull().WithMessage("Phone number is required.").NotEmpty()
                .WithMessage("Phone number is required.");
            RuleFor(x => x.Address).NotNull().WithMessage("Address is required.").NotEmpty()
               .WithMessage("Address is required.");
            RuleFor(x => x.FirstName).NotNull().WithMessage("FirstName is required.").NotEmpty()
               .WithMessage("FirstName is required.");
            RuleFor(x => x.LastName).NotNull().WithMessage("LastName is required.").NotEmpty()
              .WithMessage("LastName is required.");
        }
    }
}
