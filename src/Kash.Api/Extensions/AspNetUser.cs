using Kash.Api.Business.Interfaces;
using System.Security.Claims;

namespace Kash.Api.Extensions
{
    /// <summary>
    /// Representa um Usuário logado
    /// </summary>
    public class AspNetUSer : IUser
    {
        /// <summary>
        /// Interface do AspNetCore para acessar o HttpContext
        /// </summary>
        private readonly IHttpContextAccessor _accessor;

        /// <summary>
        /// Construtor padrão com o HttpContext
        /// </summary>
        /// <param name="accessor"></param>
        public AspNetUSer(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        /// <summary>
        /// Nome do Usuário
        /// </summary>
        public string Name => _accessor.HttpContext.User.Identity.Name;

        /// <summary>
        /// Retorna o Id do Usuário
        /// </summary>
        /// <returns></returns>
        public Guid GetUserId()
        {
            return IsAuthenticated() ? Guid.Parse(_accessor.HttpContext.User.GetUserId()) : Guid.NewGuid();
        }

        /// <summary>
        /// Retorna o Email do Usuário
        /// </summary>
        /// <returns></returns>
        public string GetUserEmail()
        {
            return IsAuthenticated() ? _accessor.HttpContext.User.GetUserEmail() : string.Empty;
        }

        /// <summary>
        /// Verifica se o Usuário está autenticado
        /// </summary>
        /// <returns></returns>
        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        /// <summary>
        /// Verifica se o Usuário possui uma Role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool IsInRole(string role)
        {
            return _accessor.HttpContext.User.IsInRole(role);
        }

        /// <summary>
        /// Retorna uma lista de Claims de um Usuário
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext.User.Claims;
        }
    }

    /// <summary>
    /// Classe de extensão do ClaimsPrincipal
    /// </summary>
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Retorna o Id do Usuário
        /// </summary>
        /// <returns></returns>
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }

            var claim = principal.FindFirst(ClaimTypes.NameIdentifier);

            return claim?.Value;
        }

        /// <summary>
        /// Retorna o Email do Usuário
        /// </summary>
        /// <returns></returns>
        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }

            var claim = principal.FindFirst(ClaimTypes.Email);

            return claim?.Value;
        }
    }
}
