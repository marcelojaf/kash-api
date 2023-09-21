using Newtonsoft.Json;

namespace Kash.Api.Business.Models.Validations.ErrorResponse
{
    /// <summary>
    /// Classe que representa um erro customizado para o sistema
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Mensagens de erro. Em geral, mais resumidas.
        /// </summary>
        [JsonProperty("error")]
        public string[] MessageList { get; set; }

        /// <summary>
        /// Descrição da exception.
        /// </summary>
        [JsonProperty("error_description")]
        public string Exception { get; set; }

        /// <summary>
        /// Código do erro.
        /// </summary>
        [JsonProperty("error_code")]
        [JsonConverter(typeof(ErrorCodeJsonConverter))]
        public uint Code { get; set; }
    }
}

