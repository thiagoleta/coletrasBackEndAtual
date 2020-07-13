using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projeto.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Mappings
{
    public class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            //nome da tabela no banco de dados (opcional)
            builder.ToTable("Cliente");

            //chave primA?ria da tabela
            //para o EF, todo campo int que for definido como chave primA?ria
            //jA? A© criado como identity (auto-incremento)
            builder.HasKey(c => c.Cod_Cliente);

            #region Mapeamento dos Relacionamentos   


            //Relacionamento um para 1
            builder.Property(c => c.Cod_Rota);
            builder.HasOne(c => c.Rota).WithMany().HasForeignKey(c => c.Cod_Rota);            

            #endregion



        }

    }
}

