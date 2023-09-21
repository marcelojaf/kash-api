namespace Kash.Api.Business.Models.Validations.Exceptions
{
    /// <summary>
    /// Classe para represntar uma exceção gerada por um Modelo (objeto)
    /// </summary>
    public class ModelStateException : Exception
    {
        /// <summary>
        /// Construtor padrão
        /// </summary>
        public ModelStateException() : base()
        {
        }

        /// <summary>
        /// Construtor com mensagem de erro
        /// </summary>
        /// <param name="message">Mensagem de erro</param>
        public ModelStateException(string message) : base(message)
        {
        }

        /// <summary>
        /// Construtor com mensagem de erro e objeto do tipo Exception
        /// </summary>
        /// <param name="message">Mensagem de erro</param>
        /// <param name="innerException">Objeto do tipo Exception</param>
        public ModelStateException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

