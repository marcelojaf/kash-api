using FluentValidation;
using Kash.Api.Business.Models;

namespace Kash.Api.Business.Services
{
    /// <summary>
    /// Classe abstrata da classe base de um Service
    /// </summary>
    public abstract class BaseService
    {
        /// <summary>
        /// Construtor padrão
        /// </summary>
        public BaseService()
        {
        }

        /// <summary>
        /// Executa uma validação customizada em uma entidade
        /// </summary>
        /// <typeparam name="TV">Tipo da Validação</typeparam>
        /// <typeparam name="TE">Tipo da Entidade</typeparam>
        /// <param name="validacao">Validação</param>
        /// <param name="entidade">Entidade</param>
        /// <returns></returns>
        protected static bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid) return true;

            return false;
        }
    }
}

