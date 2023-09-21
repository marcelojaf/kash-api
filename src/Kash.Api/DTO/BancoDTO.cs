using System.ComponentModel.DataAnnotations;

namespace Kash.Api.DTO
{
    /// <summary>
    /// Representa um Banco
    /// </summary>
    public class BancoDTO
    {
        /// <summary>
        /// Id do Banco
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// O nome do Banco
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 1)]
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// O número do Banco
        /// </summary>
        [StringLength(3, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 3)]
        public string Numero { get; set; } = string.Empty;
    }
}

