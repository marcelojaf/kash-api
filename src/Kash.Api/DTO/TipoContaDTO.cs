using System.ComponentModel.DataAnnotations;

namespace Kash.Api.DTO
{
    /// <summary>
    /// Representa um Tipo de Conta
    /// </summary>
    public class TipoContaDTO
    {
        /// <summary>
        /// Id do Tipo de Conta
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Nome do Tipo de Conta
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 1)]
        public string Nome { get; set; }
    }
}
