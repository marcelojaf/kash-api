using Newtonsoft.Json;

namespace Kash.Api.Business.Models.Validations.ErrorResponse
{
    /// <summary>
    /// Converter customizado para enviar o código do erro como string.
    /// Dessa forma, é mais fácil de ler em modo debug invés de um longo número uint.
    /// </summary>
    public class ErrorCodeJsonConverter : JsonConverter
    {
        /// <summary>
        /// Verifica se o objecto passado pelo parâmetro, pode ser associado a uma variável do tipo ErrorResponse
        /// </summary>
        /// <param name="objectType">Tipo do objeto a ser comparado</param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return typeof(ErrorResponse).IsAssignableFrom(objectType);
        }

        /// <summary>
        /// Lê um valor de um Json e tenta converter para um número UInt32
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // Converte o formato 0x{0:X} para uint
            if (reader.TokenType == JsonToken.String)
            {
                var hex = reader?.Value?.ToString();
                var ret = Convert.ToUInt32(hex, 16);
                return ret;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Serializa um valor em objeto Json
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            => serializer.Serialize(writer, string.Format("0x{0:x8}", value is null ? string.Empty : (uint)value));
    }
}

