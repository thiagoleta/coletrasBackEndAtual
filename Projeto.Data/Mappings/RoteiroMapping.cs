using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projeto.Data.Entities;
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

            //chave primmariA da tabela                        
            builder.HasKey(ro => ro.Cod_Roteiro);

            #region Relacionamentos
            //Relacionamento um para 1
            builder.Property(ro => ro.Cod_Cliente);
            builder.HasOne(ro => ro.Cliente).WithMany().HasForeignKey(c => c.Cod_Cliente);

            builder.Property(ro => ro.Cod_Turno);
            builder.HasOne(ro => ro.Turno).WithMany().HasForeignKey(c => c.Cod_Turno);

            builder.Property(ro => ro.Cod_Dia);
            builder.HasOne(ro => ro.Dias_Coleta).WithMany().HasForeignKey(c => c.Cod_Dia);

            builder.Property(ro => ro.Cod_Motorista);
            builder.HasOne(ro => ro.Motorista).WithMany().HasForeignKey(c => c.Cod_Motorista);

            builder.Property(ro => ro.Cod_Material);
            builder.HasOne(ro => ro.Material).WithMany().HasForeignKey(c => c.Cod_Material);

            #endregion
        }
    }
}
