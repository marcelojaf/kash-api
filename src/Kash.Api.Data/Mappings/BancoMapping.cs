using Kash.Api.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kash.Api.Data.Mappings
{
    /// <summary>
    /// Mapeamento da entidade do tipo Banco
    /// </summary>
    internal class BancoMapping : IEntityTypeConfiguration<Banco>
    {
        /// <summary>
        /// Inclui configurações customizadas para a entidade do tipo Banco
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Banco> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Nome)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(b => b.Numero)
                .HasColumnType("varchar(3)");

            builder.ToTable("Bancos");
        }
    }
}

