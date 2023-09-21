using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kash.Api.Data
{
    /// <summary>
    /// Context do Identity do Sistema Kash
    /// </summary>
    public class KashIdentityDbContext : IdentityDbContext
    {
        /// <summary>
        /// Construtor do Context
        /// </summary>
        /// <param name="options"></param>
        public KashIdentityDbContext(DbContextOptions<KashIdentityDbContext> options) : base(options)
        {

        }
    }
}
