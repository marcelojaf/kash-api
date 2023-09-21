using Kash.Api.Business.Models;
using System.ComponentModel.DataAnnotations;

namespace Kash.Api.DTO
{
    /// <summary>
    /// Representa uma Conta
    /// </summary>
    public class ContaDTO
    {
        /// <summary>
        /// Id da Conta
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Nome da Conta
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 1)]
        public string Nome { get; set; }

        /// <summary>
        /// Numero da Conta
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(20, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 1)]
        public string Numero { get; set; }

        /// <summary>
        /// Agencia da Conta
        /// </summary>
        public string Agencia { get; set; }

        /// <summary>
        /// Saldo da Conta
        /// </summary>
        public decimal Saldo { get; set; } = 0;

        /// <summary>
        /// Banco da Conta
        /// </summary>
        public BancoDTO Banco { get; set; }

        /// <summary>
        /// Tipo de Conta
        /// </summary>
        public TipoContaDTO TipoConta { get; set; }
    }
}
