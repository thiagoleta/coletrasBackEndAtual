using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projeto.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Mappings
{
    public class OSMapping : IEntityTypeConfiguration<OS>
    {
        public void Configure(EntityTypeBuilder<OS> builder)
        {
            //nome da tabela no banco de dados (opcional)
            builder.ToTable("OS");

            //chave primA?ria da tabela
            //para o EF, todo campo int que for definido como chave primA?ria
            //jA? A© criado como identity (auto-incremento)
            builder.HasKey(o => o.Cod_OS);

            #region Mapeamento dos Relacionamentos
            
            builder.Property(o => o.Cod_Contrato);
            builder.HasOne(o => o.Contrato).WithMany().HasForeignKey(o => o.Cod_Contrato);

            builder.Property(o => o.Cod_MesReferencia);
            builder.HasOne(o => o.MesReferencia).WithMany().HasForeignKey(o=> o.Cod_MesReferencia);

            builder.Property(c => c.Cod_Cliente);
            builder.HasMany(c => c.Clientes).WithOne().HasForeignKey(c => c.Cod_Cliente);            


            #endregion



        }

    }
}

