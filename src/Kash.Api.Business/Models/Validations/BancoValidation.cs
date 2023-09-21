using FluentValidation;

namespace Kash.Api.Business.Models.Validations
{
    /// <summary>
    /// Classe com as validações do objeto Banco
    /// </summary>
    internal class BancoValidation : AbstractValidator<Banco>
    {
        /// <summary>
        /// Construtor padrão
        /// </summary>
        public BancoValidation()
        {
            RuleFor(b => b.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                .Length(1, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres.");
        }
    }
}

