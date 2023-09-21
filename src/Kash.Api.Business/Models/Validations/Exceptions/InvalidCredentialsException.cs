using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kash.Api.Business.Models.Validations.Exceptions
{
    /// <summary>
    /// Classe para represntar uma exceção gerada por um login com credenciais inválidas
    /// </summary>
    public class InvalidCredentialsException : Exception
    {
        /// <summary>
        /// Construtor padrão
        /// </summary>
        public InvalidCredentialsException() : base()
        {
        }

        /// <summary>
        /// Construtor com mensagem de erro
        /// </summary>
        /// <param name="message">Mensagem de erro</param>
        public InvalidCredentialsException(string message) : base(message)
        {
        }

        /// <summary>
        /// Construtor com mensagem de erro e objeto do tipo Exception
        /// </summary>
        /// <param name="message">Mensagem de erro</param>
        /// <param name="innerException">Objeto do tipo Exception</param>
        public InvalidCredentialsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
