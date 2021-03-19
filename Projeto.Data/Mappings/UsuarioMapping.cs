using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projeto.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {            
            builder.ToTable("Usuario");                              
            builder.HasKey(u => u.Cod_Usuario);           
            builder.Property(u => u.Nome).HasColumnName("Nome");
            builder.Property(u => u.Email).HasColumnName("Email");
            builder.Property(u => u.Senha).HasColumnName("Senha");
            builder.Property(u => u.DataCriacao).HasColumnName("DataCriacao");


            builder.Property(u => u.Cod_Perfil);
            builder.HasOne(u => u.Perfil).WithMany().HasForeignKey(c => c.Cod_Perfil);
        }
    }
}
