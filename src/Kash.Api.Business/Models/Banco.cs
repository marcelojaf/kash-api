namespace Kash.Api.Business.Models
{
    /// <summary>
    /// Representa um Banco
    /// </summary>
    public class Banco : Entity
    {
        /// <summary>
        /// Nome do Banco
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Número do Banco
        /// </summary>
        public string Numero { get; set; }
    }
}

