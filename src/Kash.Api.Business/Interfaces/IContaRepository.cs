using Kash.Api.Business.Models;

namespace Kash.Api.Business.Interfaces
{
    /// <summary>
    /// Interface para o repositório de Conta
    /// </summary>
    public interface IContaRepository : IRepository<Conta>
    {
        /// <summary>
        /// Retorna uma lista de objetos do tipo Conta, incluindo propriedades do tipo Objeto
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Conta>> GetTodasContas();
    }
}
