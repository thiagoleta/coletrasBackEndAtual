using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projeto.Data.Entities;
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

          

            builder.Property(r => r.Cod_Motorista)
        .HasColumnName("Cod_Motorista");

       

            #region Mapeamento dos Relacionamentos            
            //Relacionamento um para 1
            builder.Property(r => r.Cod_Motorista);
            builder.HasOne(r => r.Motorista).WithMany().HasForeignKey(r => r.Cod_Motorista);

            #endregion
        }

    }
}

