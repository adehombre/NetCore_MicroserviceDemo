using CustomerAPI.Data.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Validators
{
    public class CustomerValidator: AbstractValidator<CustomerModel>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.FirstName)
                .NotNull()
                .WithMessage("The first name is required");

            RuleFor(x => x.FirstName)
                .MinimumLength(3)
                .WithMessage("The first name must be at least 3 character long");

            RuleFor(x => x.LastName)
                .NotNull()
                .WithMessage("The last name field is required");

            RuleFor(x => x.Age)
                .InclusiveBetween(0, 150)
                .WithMessage("The minimum age is 0 and the maximum is 150 years");
        }
    }
}
