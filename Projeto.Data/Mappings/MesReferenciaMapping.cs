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

            //chave primA?ria da tabela
            //para o EF, todo campo int que for definido como chave primA?ria
            //jA? A© criado como identity (auto-incremento)
            builder.HasKey(m => m.Cod_MesReferencia);

            #region Mapeamento dos Relacionamentos


            #endregion



        }

    }
}

