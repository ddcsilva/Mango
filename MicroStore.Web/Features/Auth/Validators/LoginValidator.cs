using FluentValidation;
using Mango.Web.Features.Auth.DTOs;

namespace Mango.Web.Features.Auth.Validators;

public class LoginValidator : AbstractValidator<LoginRequestDTO>
{
    public LoginValidator()
    {
        RuleFor(l => l.UserName)
            .NotEmpty().WithMessage("O usuário é obrigatório.")
            .EmailAddress().WithMessage("O usuário deve ser um e-mail válido.");

        RuleFor(l => l.Password)
            .NotEmpty().WithMessage("A senha é obrigatória.");
    }
}
