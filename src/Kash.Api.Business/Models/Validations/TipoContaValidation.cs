using FluentValidation;

namespace Kash.Api.Business.Models.Validations
{
    /// <summary>
    /// Classe com as validações do objeto TipoConta
    /// </summary>
    internal class TipoContaValidation : AbstractValidator<TipoConta>
    {
        /// <summary>
        /// Construtor padrão
        /// </summary>
        public TipoContaValidation()
        {
            RuleFor(tc => tc.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                .Length(1, 50).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres.");
        }
    }
}
