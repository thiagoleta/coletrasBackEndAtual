using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projeto.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Mappings
{
    public class Dias_ColetaMapping : IEntityTypeConfiguration<Dias_Coleta>
    {
        public void Configure(EntityTypeBuilder<Dias_Coleta> builder)
        {
            //nome da tabela no banco de dados (opcional)
            builder.ToTable("Dias_Coleta");

            //chave primA?ria da tabela
            //para o EF, todo campo int que for definido como chave primA?ria
            //jA? A© criado como identity (auto-incremento)
            builder.HasKey(d => d.Cod_Dia);

       
        }
    }
}
