using Kash.Api.Business.Interfaces;
using Kash.Api.Business.Models;
using Kash.Api.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Kash.Api.Data.Repository
{
    /// <summary>
    /// Classe repositório da entidade do tipo Conta
    /// </summary>
    public class ContaRepository : Repository<Conta>, IContaRepository
    {
        /// <summary>
        /// Construtor com Context
        /// </summary>
        /// <param name="context"></param>
        public ContaRepository(KashDbContext context) : base(context)
        {

        }

        /// <summary>
        /// Retorna uma lista de objetos do tipo Conta, incluindo propriedades do tipo Objeto
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Conta>> GetTodasContas()
        {
            return await _context.Contas
                .AsNoTracking()
                .Include(c => c.Banco)
                .Include(c => c.TipoConta)
                .OrderBy(c => c.Nome)
                .ToListAsync();
        }
    }
}
