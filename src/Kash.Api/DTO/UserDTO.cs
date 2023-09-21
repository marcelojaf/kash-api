using System.ComponentModel.DataAnnotations;

namespace Kash.Api.DTO
{
    /// <summary>
    /// Representa um Usuário
    /// </summary>
    public class UserDTO
    {
        /// <summary>
        /// Email do usuário
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }

        /// <summary>
        /// Email do usuário
        /// </summary>
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Password { get; set; }

        /// <summary>
        /// Email do usuário
        /// </summary>
        [Compare("Password", ErrorMessage = "As senhas não conferem.")]
        public string ConfirmPassword { get; set; }
    }
}
