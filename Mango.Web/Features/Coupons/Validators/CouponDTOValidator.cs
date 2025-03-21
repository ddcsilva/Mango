using FluentValidation;
using Mango.Web.Features.Coupons.DTOs;

namespace Mango.Web.Features.Coupons.Validators;

public class CouponDTOValidator : AbstractValidator<CouponDTO>
{
    public CouponDTOValidator()
    {
        RuleFor(c => c.CouponCode)
            .NotEmpty().WithMessage("O código do cupom é obrigatório.")
            .Length(3, 10).WithMessage("O código deve ter entre 3 e 10 caracteres.");

        RuleFor(c => c.DiscountAmount)
            .GreaterThan(0).WithMessage("O desconto deve ser maior que zero.");

        RuleFor(c => c.MinAmount)
            .NotNull().WithMessage("O valor mínimo é obrigatório.")
            .GreaterThanOrEqualTo(0).WithMessage("O valor mínimo deve ser positivo.");
    }
}
