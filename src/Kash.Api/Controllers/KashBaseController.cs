using Kash.Api.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Xml.Serialization;

namespace Kash.Api.Controllers
{
    /// <summary>
    /// Controller base para todo o sistema
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class KashBaseController : ControllerBase
    {
        /// <summary>
        /// Usuario do AspNetCore
        /// </summary>
        public readonly IUser Usuario;

        /// <summary>
        /// Id do Usuário logado
        /// </summary>
        protected Guid UserId { get; set; }

        /// <summary>
        /// Checa se o usuário está autenticado
        /// </summary>
        protected bool IsUserAuthenticated { get; set; }

        /// <summary>
        /// Controller padrão com Usuário
        /// </summary>
        /// <param name="usuario">Usuario do AspNetCore</param>
        public KashBaseController(IUser usuario)
        {
            Usuario = usuario;

            if (usuario.IsAuthenticated())
            {
                UserId = usuario.GetUserId();
                IsUserAuthenticated = true;
            }
        }


        /// <summary>
        /// Retorna uma lista de erros de validação de um modelo
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        protected List<string> GetModelStateErrors(ModelStateDictionary modelState)
        {
            List<string> listaDeErros = new();
            var erros = modelState.Values.SelectMany(e => e.Errors);

            foreach (var erro in erros)
            {
                listaDeErros.Add(erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message);
            }

            return listaDeErros;
        }
    }
}

