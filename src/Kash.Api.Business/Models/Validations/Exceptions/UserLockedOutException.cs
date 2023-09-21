namespace Kash.Api.Business.Models.Validations.Exceptions
{
    /// <summary>
    /// Classe para represntar uma exceção gerada por um Usuário travado
    /// </summary>
    public class UserLockedOutException : Exception
    {
        /// <summary>
        /// Construtor padrão
        /// </summary>
        public UserLockedOutException() : base()
        {
        }

        /// <summary>
        /// Construtor com mensagem de erro
        /// </summary>
        /// <param name="message">Mensagem de erro</param>
        public UserLockedOutException(string message) : base(message)
        {
        }

        /// <summary>
        /// Construtor com mensagem de erro e objeto do tipo Exception
        /// </summary>
        /// <param name="message">Mensagem de erro</param>
        /// <param name="innerException">Objeto do tipo Exception</param>
        public UserLockedOutException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
