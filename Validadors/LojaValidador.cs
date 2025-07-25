using FluentValidation;
using MiniExpress.Models;

namespace MiniExpress.Validadors
{
    public class LojaValidador : AbstractValidator<LojaModel>
    {
        public LojaValidador()
        {
            RuleFor(l => l.IdUsuario)
                .NotEmpty().WithMessage("O ID do usuário é obrigatório.")
                .GreaterThan(0).WithMessage("O ID do usuário deve ser maior que zero.");

            RuleFor(l => l.Nome)
                .NotEmpty().WithMessage("O nome da loja é obrigatório.")
                .Length(2, 100).WithMessage("O nome da loja deve ter entre 2 e 100 caracteres.");

            RuleFor(l => l.CNPJ)
                .NotEmpty().WithMessage("O CNPJ da loja é obrigatório.")
                .Length(14).WithMessage("O CNPJ da loja deve ter 14 caracteres.");

            RuleFor(l => l.Telefone)
                .NotEmpty().WithMessage("O telefone da loja é obrigatório.")
                .Length(10, 15).WithMessage("O telefone da loja deve ter entre 10 e 15 caracteres.");
        }
    }
}