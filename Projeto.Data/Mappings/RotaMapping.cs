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
         
    
        }

    }
}

