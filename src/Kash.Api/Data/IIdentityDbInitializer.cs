namespace Kash.Api.Data
{
    /// <summary>
    /// Interface para inicializar o IdentityContext
    /// </summary>
    public interface IIdentityDbInitializer
    {
        /// <summary>
        /// Inicializar o IdentityContext
        /// </summary>
        void Initialize();
    }
}
