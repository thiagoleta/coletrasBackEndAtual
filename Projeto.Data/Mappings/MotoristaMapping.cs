using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projeto.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Mappings
{
    public class MotoristaMapping : IEntityTypeConfiguration<Motorista>
    {
        public void Configure(EntityTypeBuilder<Motorista> builder)
        {
            builder.ToTable("Motorista");
            builder.HasKey(m => m.Cod_Motorista);

            builder.Property(x => x.Cod_Motorista).HasColumnName("Cod_Motorista")
           .IsRequired();


            builder.Property(bi => bi.Telefone1).HasColumnName("Telefone1");

            builder.Property(x => x.Telefone2).HasColumnName("Telefone2");

            builder.Property(bi => bi.Ajudante1).HasColumnName("Ajudante1");

            builder.Property(bi => bi.Ajudante2).HasColumnName("Ajudante2");

            builder.Property(bi => bi.Placa).HasColumnName("Placa").IsRequired();


        }

    }
}

