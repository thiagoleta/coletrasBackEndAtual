using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projeto.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Mappings
{
    public class ConfigucacaoMapping : IEntityTypeConfiguration<Configucacao>
    {
        public void Configure(EntityTypeBuilder<Configucacao> builder)
        {
            //nome da tabela no banco de dados (opcional)
            builder.ToTable("Configucacao");

            //chave primA?ria da tabela
            //para o EF, todo campo int que for definido como chave primA?ria
            //jA? A© criado como identity (auto-incremento)
            builder.HasKey(c => c.Cod_Configucacao);

            #region Mapeamento dos Relacionamentos


            #endregion



        }

    }
}

