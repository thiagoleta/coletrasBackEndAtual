﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projeto.Data.Entities;
using Projeto.Data.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Mappings
{
    public class RoteiroMapping : IEntityTypeConfiguration<Roteiro>
    {
        public void Configure(EntityTypeBuilder<Roteiro> builder)
        {
            //nome da tabela no banco de dados (opcional)
            builder.ToTable("Roteiro");

            builder.HasKey(ro => ro.Cod_Roteiro);
            builder.Property(ro => ro.Cod_Roteiro).HasColumnName("Cod_Roteiro").HasDefaultValue(null);


            builder.Property(ro => ro.Cod_Cliente).HasColumnName("Cod_Cliente").IsRequired();
            builder.Property(ro => ro.Cod_Turno).HasColumnName("Cod_Turno").IsRequired();
            builder.Property(ro => ro.Cod_Motorista).HasColumnName("Cod_Motorista").IsRequired();
            builder.Property(ro => ro.Cod_Material).HasColumnName("Cod_Material").IsRequired();

            builder.Property(ro => ro.Segunda).HasColumnName("Segunda").HasConversion(ValueConverters.BoolToString).HasDefaultValue(false);
            builder.Property(ro => ro.Terca).HasColumnName("Terca").HasConversion(ValueConverters.BoolToString).HasDefaultValue(false);
            builder.Property(ro => ro.Quarta).HasColumnName("Quarta").HasConversion(ValueConverters.BoolToString).HasDefaultValue(false);
            builder.Property(ro => ro.Quinta).HasColumnName("Quinta").HasConversion(ValueConverters.BoolToString).HasDefaultValue(false);
            builder.Property(ro => ro.Sexta).HasColumnName("Sexta").HasConversion(ValueConverters.BoolToString).HasDefaultValue(false);
            builder.Property(ro => ro.Sabado).HasColumnName("Sabado").HasConversion(ValueConverters.BoolToString).HasDefaultValue(false);
            builder.Property(ro => ro.Domingo).HasColumnName("Domingo").HasConversion(ValueConverters.BoolToString).HasDefaultValue(false);

            builder.Property(ro => ro.Observacao).HasColumnName("Observacao");

            #region Relacionamentos
            //Relacionamento um para 1
            builder.Property(ro => ro.Cod_Cliente);
            builder.HasOne(ro => ro.Cliente).WithMany().HasForeignKey(c => c.Cod_Cliente);

            builder.Property(ro => ro.Cod_Turno);
            builder.HasOne(ro => ro.Turno).WithMany().HasForeignKey(c => c.Cod_Turno);

            builder.Property(ro => ro.Cod_Motorista);
            builder.HasOne(ro => ro.Motorista).WithMany().HasForeignKey(c => c.Cod_Motorista);

            builder.Property(ro => ro.Cod_Material);
            builder.HasOne(ro => ro.Material).WithMany().HasForeignKey(c => c.Cod_Material);

            #endregion
        }
    }
}

