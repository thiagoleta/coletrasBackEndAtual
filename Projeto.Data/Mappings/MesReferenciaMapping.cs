using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projeto.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Mappings
{
    public class MesReferenciaMapping : IEntityTypeConfiguration<MesReferencia>
    {
        public void Configure(EntityTypeBuilder<MesReferencia> builder)
        {
            //nome da tabela no banco de dados (opcional)
            builder.ToTable("MesReferencia");
            
            builder.HasKey(m => m.Cod_MesReferencia);     

        }

    }
}

