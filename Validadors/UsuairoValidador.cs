using FluentValidation;
using MiniExpress.Models;

namespace MiniExpress.Validadors
{
    public class UsuarioValidador : AbstractValidator<UsuarioModel>
    {
        public UsuarioValidador()
        {
            RuleFor(u => u.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .Length(2, 100).WithMessage("O nome deve ter entre 2 e 100 caracteres.");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("O email é obrigatório.")
                .EmailAddress().WithMessage("O email deve ser um endereço de email válido.");

            RuleFor(u => u.SenhaHash)
                .NotEmpty().WithMessage("A senha é obrigatória.")
                .MinimumLength(6).WithMessage("A senha deve ter pelo menos 6 caracteres.");
        }
    }
}