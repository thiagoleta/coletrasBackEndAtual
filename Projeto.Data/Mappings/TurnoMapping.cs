using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projeto.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Mappings
{
    public class TurnoMapping : IEntityTypeConfiguration<Turno>
    {
        public void Configure(EntityTypeBuilder<Turno> builder)
        {
            //nome da tabela no banco de dados (opcional)
            builder.ToTable("Turno");

            //chave primmariA da tabela                        
            builder.HasKey(t => t.Cod_Turno);
        }
    }
}
