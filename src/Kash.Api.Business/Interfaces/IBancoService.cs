using Kash.Api.Business.Models;

namespace Kash.Api.Business.Interfaces
{
    /// <summary>
    /// Interface para o serviço de Banco
    /// </summary>
    public interface IBancoService : IDisposable
    {
        /// <summary>
        /// Insere um Banco
        /// </summary>
        /// <param name="banco">Objeto do tipo Banco a ser inserido.</param>
        /// <returns></returns>
        Task Insert(Banco banco);

        /// <summary>
        /// Atualiza um Banco
        /// </summary>
        /// <param name="banco">Objeto do tipo Banco a ser atualizado</param>
        /// <returns></returns>
        Task Update(Banco banco);

        /// <summary>
        /// Remove um Banco
        /// </summary>
        /// <param name="id">Id do Banco a ser removido</param>
        /// <returns></returns>
        Task Delete(Guid id);
    }
}

