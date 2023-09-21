namespace Kash.Api.Business.Models
{
    /// <summary>
    /// Classe abstrata para representar uma Entidade (objeto) do banco de dados
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Flag que determina se esse registro está ativo ou não
        /// </summary>
        public bool Ativo { get; set; } = true;

        /// <summary>
        /// Data de Cadastro do registro
        /// </summary>
        public DateTime DataCadastro { get; set; }
    }
}

