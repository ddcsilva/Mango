using FluentValidation;
using MicroStore.Web.Features.Auth.DTOs;

namespace MicroStore.Web.Features.Auth.Validators;

public class RegisterValidator : AbstractValidator<RegistrationRequestDTO>
{
    public RegisterValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty().WithMessage("Nome é obrigatório.")
            .MinimumLength(3).WithMessage("Nome deve ter no mínimo 3 caracteres.");

        RuleFor(r => r.Email)
            .NotEmpty().WithMessage("E-mail é obrigatório.")
            .EmailAddress().WithMessage("E-mail inválido.");

        RuleFor(r => r.PhoneNumber)
            .NotEmpty().WithMessage("Telefone é obrigatório.")
            .Matches(@"^(\+[0-9]{9})$").WithMessage("Telefone inválido.");

        RuleFor(r => r.Password)
            .NotEmpty().WithMessage("Senha é obrigatória.")
            .MinimumLength(6).WithMessage("Senha deve ter no mínimo 6 caracteres.")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$").WithMessage("Senha deve conter pelo menos uma letra maiúscula, uma letra minúscula e um número.");
    }
}
