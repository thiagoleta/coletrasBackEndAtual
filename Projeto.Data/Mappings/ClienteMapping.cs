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

            builder.HasOne(r => r.Rota).WithMany().HasForeignKey(r => r.Cod_Rota);

            #endregion



        }

    }
}

