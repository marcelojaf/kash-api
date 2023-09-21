using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Kash.Api.Extensions
{
    /// <summary>
    /// Classe de Custom Authorization
    /// </summary>
    public class CustomAuthorization
    {
        /// <summary>
        /// Valida se o Usuário possui uma Claim
        /// </summary>
        /// <param name="context"></param>
        /// <param name="claimName">Nome da Claim</param>
        /// <param name="claimValue">Valor da Claim</param>
        /// <returns></returns>
        public static bool ValidarClaimsUsuario(HttpContext context, string claimName, string claimValue)
        {
            return context.User.Identity.IsAuthenticated &&
                   context.User.Claims.Any(c => c.Type == claimName && c.Value.Contains(claimValue));
        }

    }

    /// <summary>
    /// Classe de ClaimsAuthorizeAttribute
    /// </summary>
    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// Construtor padrão com Claim
        /// </summary>
        /// <param name="claimName">Nome da Claim</param>
        /// <param name="claimValue">Valor da Claim</param>
        public ClaimsAuthorizeAttribute(string claimName, string claimValue) : base(typeof(RequisitoClaimFilter))
        {
            Arguments = new object[] { new Claim(claimName, claimValue) };
        }
    }

    /// <summary>
    /// Classe de RequisitoClaimFilter
    /// </summary>
    public class RequisitoClaimFilter : IAuthorizationFilter
    {
        private readonly Claim _claim;

        /// <summary>
        /// Construtor padrão
        /// </summary>
        /// <param name="claim"></param>
        public RequisitoClaimFilter(Claim claim)
        {
            _claim = claim;
        }

        /// <summary>
        /// Verifica se o Usuário tem acesso a claim passada
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new StatusCodeResult(401);
                return;
            }

            if (!CustomAuthorization.ValidarClaimsUsuario(context.HttpContext, _claim.Type, _claim.Value))
            {
                context.Result = new StatusCodeResult(403);
            }
        }
    }
}
