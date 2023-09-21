using System.Security.Claims;

namespace Kash.Api.Business.Interfaces
{
    /// <summary>
    /// Interface para Usuário autenticado
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// Nome do Usuário
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Retorna o Id do Usuário
        /// </summary>
        /// <returns></returns>
        Guid GetUserId();

        /// <summary>
        /// Retorna o Email do Usuário
        /// </summary>
        /// <returns></returns>
        string GetUserEmail();

        /// <summary>
        /// Verifica se o Usuário está autenticado
        /// </summary>
        /// <returns></returns>
        bool IsAuthenticated();

        /// <summary>
        /// Verifica se o Usuário possui uma Role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        bool IsInRole(string role);

        /// <summary>
        /// Retorna uma lista de Claims de um Usuário
        /// </summary>
        /// <returns></returns>
        IEnumerable<Claim> GetClaimsIdentity();
    }
}
