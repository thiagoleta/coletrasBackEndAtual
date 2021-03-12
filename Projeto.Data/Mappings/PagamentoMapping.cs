using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projeto.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Mappings
{
    public class PagamentoMapping : IEntityTypeConfiguration<Pagamento>
    {
        public void Configure(EntityTypeBuilder<Pagamento> builder)
        {
            //nome da tabela no banco de dados (opcional)
            builder.ToTable("Pagamento");

            //chave primA?ria da tabela
            //para o EF, todo campo int que for definido como chave primA?ria
            //jA? A© criado como identity (auto-incremento)
            builder.HasKey(p => p.Cod_Pagamento);

            builder.Property(x => x.Valor).HasColumnName("Valor");
            builder.Property(x => x.Data).HasColumnName("Data");
            builder.Property(x => x.Cod_MesReferencia).HasColumnName("Cod_MesReferencia").IsRequired();
            builder.Property(x => x.Cod_Cliente).HasColumnName("Cod_Cliente").IsRequired();

            #region Mapeamento dos Relacionamentos


            builder.HasOne(m => m.MesReferencia).WithMany().HasForeignKey(m => m.Cod_MesReferencia);
            builder.HasOne(c => c.Cliente).WithMany().HasForeignKey(c => c.Cod_Cliente);

            #endregion



        }

    }
}

