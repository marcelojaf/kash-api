using Kash.Api.Business.Models;

namespace Kash.Api.Business.Interfaces
{
    /// <summary>
    /// Interface para o serviço de Conta
    /// </summary>
    public interface IContaService : IDisposable
    {
        /// <summary>
        /// Insere uma Conta
        /// </summary>
        /// <param name="conta">Objeto do tipo Conta a ser inserido.</param>
        /// <returns></returns>
        Task Insert(Conta conta);

        /// <summary>
        /// Atualiza uma Conta
        /// </summary>
        /// <param name="conta">Objeto do tipo Conta a ser atualizado</param>
        /// <returns></returns>
        Task Update(Conta conta);

        /// <summary>
        /// Remove uma Conta
        /// </summary>
        /// <param name="id">Id da Conta a ser removida</param>
        /// <returns></returns>
        Task Delete(Guid id);
    }
}
