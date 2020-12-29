using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projeto.Data.Entities;
using Projeto.Data.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Mappings
{
    public class RotaMapping : IEntityTypeConfiguration<Rota>
    {
        public void Configure(EntityTypeBuilder<Rota> builder)
        {
            //nome da tabela (opcional)
            builder.ToTable("Rota");
            //chave primária (obrigatório)
            builder.HasKey(r => r.Cod_Rota);

            builder.Property(x => x.Nome).HasColumnName("Nome");
            builder.Property(x => x.Composicao_Rota).HasColumnName("Composicao_Rota");
            builder.Property(x => x.Flag_Ativo).HasColumnName("Flag_Ativo").HasConversion(ValueConverters.BoolToString).HasDefaultValue(false);
            builder.Property(x => x.Observacao).HasColumnName("Observacao");

        }

    }
}

