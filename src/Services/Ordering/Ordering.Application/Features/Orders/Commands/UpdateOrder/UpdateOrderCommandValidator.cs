using FluentValidation;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
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