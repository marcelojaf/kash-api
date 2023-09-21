using Kash.Api.Business.Interfaces;
using Kash.Api.Business.Models;
using Kash.Api.Data.Context;

namespace Kash.Api.Data.Repository
{
    /// <summary>
    /// Classe repositório da entidade do tipo TipoConta
    /// </summary>
    public class TipoContaRepository : Repository<TipoConta>, ITipoContaRepository
    {
        /// <summary>
        /// Construtor com Context
        /// </summary>
        /// <param name="context"></param>
        public TipoContaRepository(KashDbContext context) : base(context)
        {

        }
    }
}
