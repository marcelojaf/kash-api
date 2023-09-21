using System.ComponentModel.DataAnnotations;

namespace Kash.Api.DTO
{
    /// <summary>
    /// Representa um Login de Usuário
    /// </summary>
    public class LoginUserDTO
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
    }
    
    /// <summary>
    /// Representa a resposta do Login de Usuário
    /// </summary>
    public class LoginResponseDTO
    {
        /// <summary>
        /// O token de acesso
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Expiração do token em minutos
        /// </summary>
        public double ExpiresIn { get; set; }

        /// <summary>
        /// Objeto com informações de usuário e suas Claims
        /// </summary>
        public UserTokenDTO UserToken { get; set; }
    }

    /// <summary>
    /// Representa um Usuário autenticado
    /// </summary>
    public class UserTokenDTO
    {
        /// <summary>
        /// Id do Usuário
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Email do Usuário
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Lista de Claims do Usuário
        /// </summary>
        public IEnumerable<ClaimDTO> Claims { get; set; }
    }

    /// <summary>
    /// Representa uma Claim de um Usuário
    /// </summary>
    public class ClaimDTO
    {
        /// <summary>
        /// Tipo da Claim
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Valor da Claim
        /// </summary>
        public string Value { get; set; }
    }
}
