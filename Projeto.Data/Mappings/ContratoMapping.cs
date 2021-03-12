using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projeto.Data.Entities;
using Projeto.Data.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Mappings
{
    public class ContratoMapping : IEntityTypeConfiguration<Contrato>
    {
        public void Configure(EntityTypeBuilder<Contrato> builder)
        {

            builder.ToTable("Contrato");
            builder.HasKey(x => x.Cod_Contrato);
            builder.Property(x => x.Cod_Contrato).HasColumnName("Cod_Contrato").HasDefaultValue(null);

            #region Mapeamento dos Relacionamentos


            builder.Property(c => c.Cod_Cliente).HasColumnName("Cod_Cliente").HasDefaultValue(null);
            builder.Property(c => c.ColetaContratada).HasColumnName("Coleta_Contratada").IsRequired();

            builder.Property(c => c.ValorLimite).HasColumnName("Valor_Limite");
            builder.Property(c => c.ValorUnidade).HasColumnName("Valor_Unidade");           
                        
            builder.Property(c => c.FlagTermino).HasColumnName("Flag_Termino").HasConversion(ValueConverters.BoolToString).HasDefaultValue(false);
            builder.Property(x => x.MotivoCancelamento).HasColumnName("Motivo_Cancelamento");
            builder.Property(x => x.DataCancelamento).HasColumnName("Data_Cancelamento");


            builder.Property(x => x.DataInicio).HasColumnName("Data_Inicio");
            builder.Property(x => x.DataTermino).HasColumnName("Data_Termino");

            builder.Property(c => c.Cod_Cliente);
            builder.HasOne(c => c.Cliente).WithMany().HasForeignKey(c => c.Cod_Cliente);


            #endregion



        }

    }
}

