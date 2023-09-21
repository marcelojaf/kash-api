using Kash.Api.Business.Interfaces;
using Kash.Api.Business.Models;
using Kash.Api.Data.Context;

namespace Kash.Api.Data.Repository
{
    /// <summary>
    /// Classe repositório da entidade do tipo Banco
    /// </summary>
    public class BancoRepository : Repository<Banco>, IBancoRepository
    {
        /// <summary>
        /// Construtor com Context
        /// </summary>
        /// <param name="context"></param>
        public BancoRepository(KashDbContext context) : base(context)
        {
        }
    }
}

