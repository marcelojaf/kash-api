using Kash.Api.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace Kash.Api.Data.Context
{
    /// <summary>
    /// Context do sistema Kash
    /// </summary>
    public class KashDbContext : DbContext
    {
        /// <summary>
        /// Construtor do Context
        /// </summary>
        /// <param name="options">Opções de configuração do Context</param>
        public KashDbContext(DbContextOptions<KashDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// DbSet do tipo Banco
        /// </summary>
        public DbSet<Banco> Bancos { get; set; }

        /// <summary>
        /// DbSet do tipo TipoConta
        /// </summary>
        public DbSet<TipoConta> TiposConta { get; set; }

        /// <summary>
        /// DbSet do tipo Conta
        /// </summary>
        public DbSet<Conta> Contas { get; set; }

        /// <summary>
        /// Executa tarefas antes de criar os modelos no banco de dados
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            {
                if (property.GetMaxLength() == null)
                {
                    property.SetMaxLength(500);
                }

                if (string.IsNullOrEmpty(property.GetColumnType()))
                {
                    property.SetColumnType("varchar");
                }
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(KashDbContext).Assembly);

            // Evitar o delete cascade
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Preenche automativamente o campo DataCadastro das tabelas
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

