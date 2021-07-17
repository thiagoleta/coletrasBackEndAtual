using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projeto.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Mappings
{
   public class FrotaMapping : IEntityTypeConfiguration<Frota>
    {
        public void Configure(EntityTypeBuilder<Frota> builder) {

            builder.ToTable("Frota");
            builder.HasKey(f => f.Cod_Frota);

            builder.Property(f => f.Cod_Frota).HasColumnName("Cod_Frota").HasDefaultValue(null);

            builder.Property(f => f.Cod_Motorista).HasColumnName("Cod_Motorista").IsRequired();
            builder.Property(f => f.Descricao).HasColumnName("Descricao").IsRequired();
            builder.Property(f => f.Placa).HasColumnName("Placa").IsRequired();
            builder.Property(f => f.Observacao).HasColumnName("Observacao");
            builder.Property(f => f.Quilometragem).HasColumnName("Quilometragem");

            builder.Property(f => f.Cod_Motorista);
            builder.HasOne(f => f.Motorista).WithMany().HasForeignKey(f => f.Cod_Motorista);

        }

    }
}
