namespace Kash.Api.Extensions
{
    /// <summary>
    /// Configurações do JWT
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Chave de criptografia do token
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// Em quantas horas o token perde a validade
        /// </summary>
        public int ExpiracaoEmHoras { get; set; }

        /// <summary>
        /// Quem emite o token
        /// </summary>
        public string Emissor { get; set; }

        /// <summary>
        /// Em quais url's o token é válido
        /// </summary>
        public string ValidoEm { get; set; }
    }
}
