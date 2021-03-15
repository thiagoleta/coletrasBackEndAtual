﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projeto.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Mappings
{
    public class PerfilMapping : IEntityTypeConfiguration<Perfil>
    {
        public void Configure(EntityTypeBuilder<Perfil> builder)
        {
            //nome da tabela no banco de dados (opcional)
            builder.ToTable("Perfil");

            //chave primmariA da tabela                        
            builder.HasKey(p => p.Cod_Perfil);
            builder.Property(p => p.Cod_Usuario).HasColumnName("Cod_Usuario").IsRequired();
            builder.Property(x => x.Nome_Perfil).HasColumnName("Nome_Perfil");
            #region MyRegion

            builder.HasOne(p => p.Usuario).WithMany().HasForeignKey(c => c.Cod_Usuario);
            #endregion


        }
    }
}
