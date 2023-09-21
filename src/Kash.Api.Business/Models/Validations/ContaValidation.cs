using FluentValidation;

namespace Kash.Api.Business.Models.Validations
{
    /// <summary>
    /// Classe com as validações do objeto Conta
    /// </summary>
    internal class ContaValidation : AbstractValidator<Conta>
    {
        /// <summary>
        /// Construtor padrão
        /// </summary>
        public ContaValidation()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                .Length(1, 50).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres.");

            RuleFor(c => c.Numero)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                .Length(1, 20).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres.");
        }
    }
}
