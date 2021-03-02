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

            //Relacionamento um para 1
            builder.Property(c => c.CodCliente).HasColumnName("Cod_Cliente").IsRequired();
            builder.Property(c => c.ColetaContratada).HasColumnName("Coleta_Contratada").IsRequired();

            builder.Property(c => c.ValorLimite).HasColumnName("Valor_Limite");
            builder.Property(c => c.ValorUnidade).HasColumnName("Valor_Unidade");            

            builder.Property(x => x.FlagTermino).HasColumnName("Flag_Termino");
            builder.Property(x => x.MotivoCancelamento).HasColumnName("Motivo_Cancelamento");
            builder.Property(x => x.DataCancelamento).HasColumnName("Data_Cancelamento");


            builder.Property(x => x.DataInicio).HasColumnName("Data_Inicio");
            builder.Property(x => x.DataTermino).HasColumnName("Data_Termino");

            builder.HasOne(c => c.Cliente).WithMany().HasForeignKey(c => c.CodCliente);


            #endregion



        }

    }
}

