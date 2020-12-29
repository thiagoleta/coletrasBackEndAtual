using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projeto.Data.Entities;
using Projeto.Data.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Mappings
{
    public class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            
			builder.ToTable("Cliente");

			builder.HasKey(x => x.Cod_Cliente);
			builder.Property(x => x.Cod_Cliente).HasColumnName("Cod_Cliente").HasDefaultValue(null); ;
			


			builder.Property(x => x.NomeCompleto_RazaoSocial).HasColumnName("NomeCompleto_RazaoSocial");
			builder.Property(x => x.Fantasia).HasColumnName("Fantasia");
			builder.Property(x => x.Insc_Estadual).HasColumnName("Insc_Estadual");
			builder.Property(x => x.Insc_Estadual).HasColumnName("Insc_Estadual");
			builder.Property(x => x.Logradouro).HasColumnName("Logradouro");
			builder.Property(x => x.Endereco).HasColumnName("Endereco");
			builder.Property(x => x.Bairro).HasColumnName("Bairro");
			builder.Property(x => x.Complemento).HasColumnName("Complemento");
			builder.Property(x => x.Cidade).HasColumnName("Cidade");
			builder.Property(x => x.CEP).HasColumnName("CEP");
			builder.Property(x => x.UF).HasColumnName("UF");
			builder.Property(x => x.Telefones).HasColumnName("Telefones");
			builder.Property(x => x.Funcao).HasColumnName("Funcao");
			builder.Property(x => x.Email).HasColumnName("Email");
			builder.Property(x => x.Flag_Ativo).HasColumnName("Flag_Ativo").HasConversion(ValueConverters.BoolToString).HasDefaultValue(false);

			builder.Property(x => x.Observacao).HasColumnName("Observacao");
			builder.Property(x => x.Referencia).HasColumnName("Referencia");
			 

	}

    }
}

