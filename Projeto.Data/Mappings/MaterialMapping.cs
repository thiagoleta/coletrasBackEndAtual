using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projeto.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Mappings
{
    public class MaterialMapping : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            
            builder.ToTable("Material");           
            builder.HasKey(m => m.Cod_Material);

            builder.Property(m => m.Cod_Material).HasColumnName("Cod_Material").IsRequired();

            builder.Property(m => m.Descricao).HasColumnName("Descricao");
            builder.Property(m => m.Volume).HasColumnName("Volume");
            builder.Property(m => m.Observacao).HasColumnName("Observacao");
            builder.Property(m => m.Material_Coletado).HasColumnName("Material_Coletado");
     
    }

    }
}

