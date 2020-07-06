using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projeto.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Mappings
{
    public class ContratoMapping : IEntityTypeConfiguration<Contrato>
    {
        public void Configure(EntityTypeBuilder<Contrato> builder)
        {
            //nome da tabela no banco de dados (opcional)
            builder.ToTable("Contrato");

            //chave primA?ria da tabela
            //para o EF, todo campo int que for definido como chave primA?ria
            //jA? A© criado como identity (auto-incremento)
            builder.HasKey(c => c.Cod_Contrato);

            #region Mapeamento dos Relacionamentos

            builder.HasOne(c => c.Cliente).WithMany().HasForeignKey(c => c.Cod_Cliente);
            builder.HasOne(m => m.Material).WithMany().HasForeignKey(m => m.Cod_Material);

            #endregion



        }

    }
}

