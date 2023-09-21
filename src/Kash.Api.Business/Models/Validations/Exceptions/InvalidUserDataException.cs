using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kash.Api.Business.Models.Validations.Exceptions
{
    public class InvalidUserDataException : Exception
    {
        /// <summary>
        /// Construtor padrão
        /// </summary>
        public InvalidUserDataException() : base()
        {
        }

        /// <summary>
        /// Construtor com mensagem de erro
        /// </summary>
        /// <param name="message">Mensagem de erro</param>
        public InvalidUserDataException(string message) : base(message)
        {
        }

        /// <summary>
        /// Construtor com mensagem de erro e objeto do tipo Exception
        /// </summary>
        /// <param name="message">Mensagem de erro</param>
        /// <param name="innerException">Objeto do tipo Exception</param>
        public InvalidUserDataException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
