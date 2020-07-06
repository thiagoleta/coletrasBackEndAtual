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
            //nome da tabela no banco de dados (opcional)
            builder.ToTable("Motorista");

            //chave primária da tabela
            //para o EF, todo campo int que for definido como chave primária
            //já é criado como identity (auto-incremento)
            builder.HasKey(m => m.Cod_Motorista);




            //builder.Property(m => m.Cod_Motorista)
            // .HasColumnName("Cod_Motorista");



        }

    }
}

