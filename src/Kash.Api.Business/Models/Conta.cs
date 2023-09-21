using System.ComponentModel.DataAnnotations.Schema;

namespace Kash.Api.Business.Models
{
    /// <summary>
    /// Representa uma Conta
    /// </summary>
    public class Conta : Entity
    {
        /// <summary>
        /// Nome da Conta
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Numero da Conta
        /// </summary>
        public string Numero { get; set; }

        /// <summary>
        /// Agencia da Conta
        /// </summary>
        public string Agencia { get; set; }

        /// <summary>
        /// Saldo da Conta
        /// </summary>
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Saldo { get; set; } = 0;

        /// <summary>
        /// Banco da Conta
        /// </summary>
        public Banco Banco { get; set; }

        /// <summary>
        /// Tipo de Conta
        /// </summary>
        public TipoConta TipoConta { get; set; }
    }
}
