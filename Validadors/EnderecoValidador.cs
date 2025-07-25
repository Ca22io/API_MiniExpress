using FluentValidation;
using MiniExpress.Models;

namespace MiniExpress.Validadors
{
    public class EnderecoValidador : AbstractValidator<EnderecoModel>
    {
        public EnderecoValidador()
        {
            RuleFor(e => e.IdUsuario)
                .GreaterThan(0).WithMessage("O ID do usuário deve ser maior que zero.");

            RuleFor(e => e.IdLoja)
                .GreaterThan(0).WithMessage("O ID da loja deve ser maior que zero.");

            RuleFor(e => e.Logradouro)
                .NotEmpty().WithMessage("O logradouro é obrigatório.")
                .Length(2, 100).WithMessage("O logradouro deve ter entre 2 e 100 caracteres.");

            RuleFor(e => e.Numero)
                .NotEmpty().WithMessage("O número é obrigatório.");

            RuleFor(e => e.CEP)
                .NotEmpty().WithMessage("O CEP é obrigatório.")
                .Length(8).WithMessage("O CEP deve ter 8 caracteres.");

            RuleFor(e => e.Cidade)
                .NotEmpty().WithMessage("A cidade é obrigatória.")
                .Length(2, 50).WithMessage("A cidade deve ter entre 2 e 50 caracteres.");

            RuleFor(e => e.Estado)
                .NotEmpty().WithMessage("O estado é obrigatório.")
                .Length(2, 50).WithMessage("O estado deve ter entre 2 e 50 caracteres.");

            RuleFor(e => e.Bairro)
                .NotEmpty().WithMessage("O bairro é obrigatório.")
                .Length(2, 50).WithMessage("O bairro deve ter entre 2 e 50 caracteres.");
                
            RuleFor(e => e.Complemento)
                .MaximumLength(100).WithMessage("O complemento deve ter no máximo 100 caracteres.");
        }
    }
}