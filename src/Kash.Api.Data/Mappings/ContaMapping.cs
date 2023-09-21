using Kash.Api.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kash.Api.Data.Mappings
{
    /// <summary>
    /// Mapeamento da entidade do tipo Conta
    /// </summary>
    internal class ContaMapping : IEntityTypeConfiguration<Conta>
    {
        /// <summary>
        /// Inclui configurações customizadas para a entidade do tipo Conta
        /// </summary>
        /// <param name="builder"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Configure(EntityTypeBuilder<Conta> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(b => b.Numero)
                .HasColumnType("varchar(20)");

            builder.ToTable("Contas");
        }
    }
}
