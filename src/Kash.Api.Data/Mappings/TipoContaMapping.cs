using Kash.Api.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kash.Api.Data.Mappings
{
    /// <summary>
    /// Mapeamento da entidade do tipo TipoConta
    /// </summary>
    internal class TipoContaMapping : IEntityTypeConfiguration<TipoConta>
    {
        /// <summary>
        /// Inclui configurações customizadas para a entidade do tipo TipoConta
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<TipoConta> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Nome)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.ToTable("TiposConta");
        }
    }
}
