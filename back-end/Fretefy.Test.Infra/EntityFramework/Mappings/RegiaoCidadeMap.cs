using Fretefy.Test.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fretefy.Test.Infra.EntityFramework.Mappings
{
    public class RegiaoCidadeMap : IEntityTypeConfiguration<RegiaoCidade>
    {
        public void Configure(EntityTypeBuilder<RegiaoCidade> builder)
        {
            builder.HasKey(x => new { x.CidadeId, x.RegiaoId });
            builder.Property(p => p.Status);

            builder
                .HasOne(x=>x.Regiao)
                .WithMany(x=>x.RegiaoCidades)
                .HasForeignKey(x=>x.RegiaoId);
            
            builder
                .HasOne(x=>x.Cidade)
                .WithMany(x=>x.RegiaoCidades)
                .HasForeignKey(x=>x.CidadeId);
        }
    }
}