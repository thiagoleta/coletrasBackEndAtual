using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projeto.Data.Entities;
using Projeto.Data.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Mappings
{
    public class MesReferenciaMapping : IEntityTypeConfiguration<MesReferencia>
    {
        public void Configure(EntityTypeBuilder<MesReferencia> builder)
        {
            builder.ToTable("MesReferencia");

            builder.HasKey(x => x.Cod_MesReferencia);
            builder.Property(x => x.Cod_MesReferencia).HasColumnName("Cod_MesReferencia").IsRequired();
            builder.Property(x => x.MesAno).HasColumnName("MesAno");
            builder.Property(x => x.DataInicio).HasColumnName("Data_Inicio");
            builder.Property(x => x.DataTermino).HasColumnName("Data_Termino");
            builder.Property(x => x.Ativo).HasColumnName("Flag_Encerramento").HasConversion(ValueConverters.BoolToString).HasDefaultValue(false);


        }

    }
}

