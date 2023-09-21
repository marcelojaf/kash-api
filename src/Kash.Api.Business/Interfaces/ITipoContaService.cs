using Kash.Api.Business.Models;

namespace Kash.Api.Business.Interfaces
{
    /// <summary>
    /// Interface para o serviço de TipoConta
    /// </summary>
    public interface ITipoContaService : IDisposable
    {
        /// <summary>
        /// Insere um Tipo de Conta
        /// </summary>
        /// <param name="tipoConta">Objeto do tipo TipoConta a ser inserido.</param>
        /// <returns></returns>
        Task Insert(TipoConta tipoConta);

        /// <summary>
        /// Atualiza um Tipo de Conta
        /// </summary>
        /// <param name="tipoConta">Objeto do tipo TipoConta a ser atualizado</param>
        /// <returns></returns>
        Task Update(TipoConta tipoConta);

        /// <summary>
        /// Remove um Tipo de Conta
        /// </summary>
        /// <param name="id">Id do TipoConta a ser removido</param>
        /// <returns></returns>
        Task Delete(Guid id);
    }
}
