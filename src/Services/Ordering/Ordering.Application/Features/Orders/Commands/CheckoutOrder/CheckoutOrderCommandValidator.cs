using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandValidator: AbstractValidator<CheckoutOrderCommand>
    {
        public CheckoutOrderCommandValidator()
        {
            RuleFor(p => p.UserName)
                .NotNull()
                .NotEmpty().WithMessage("{Username} is required.")
                .MaximumLength(30).WithMessage("{UserName} must not be exceed 30 characters.");

            RuleFor(p => p.TotalPrice)
               .NotEmpty().WithMessage("{TotalPrice} is required.")
               .GreaterThan(0).WithMessage("{TotalPrice} should be greater than zero.");

            RuleFor(p => p.EmailAddress)
               .NotNull()
               .NotEmpty().WithMessage("{EmailAddress} is required.")
               .MinimumLength(10).WithMessage("{EmailAddress}  should be greater than 10 characters.");

        }
    }
}
