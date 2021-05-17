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
            builder.HasKey(o => o.Cod_OS);

            builder.Property(x => x.Data_Geracao).HasColumnName("Data_Geracao");
            builder.Property(x => x.Data_Coleta).HasColumnName("Data_Coleta");
            builder.Property(x => x.Quantidade_Coletada).HasColumnName("Quantidade_Coletada");
            
            builder.Property(x => x.Hora_Entrada).HasColumnName("Hora_Entrada");
            builder.Property(x => x.Hora_Saida).HasColumnName("Hora_Saida");
            builder.Property(x => x.Placa).HasColumnName("Placa");
            builder.Property(x => x.Motivo_Cancelamento).HasColumnName("Motivo_Cancelamento");
            builder.Property(x => x.Data_Cancelamento).HasColumnName("Data_Cancelamento");
            builder.Property(c => c.Flag_Coleta).HasColumnName("Flag_Coleta");
            builder.Property(c => c.Flag_Envio_Email).HasColumnName("Flag_Envio_Email");
            builder.Property(c => c.Flag_Cancelado).HasColumnName("Flag_Cancelado");


            builder.Property(c => c.Cod_Motorista).HasColumnName("Cod_Motorista").HasDefaultValue(null);
            builder.Property(c => c.Cod_Material).HasColumnName("Cod_Material").HasDefaultValue(null);

            builder.Property(c => c.Cod_MesReferencia).HasColumnName("Cod_MesReferencia").HasDefaultValue(null);
            builder.Property(c => c.Cod_Cliente).HasColumnName("Cod_Cliente").HasDefaultValue(null);


            #region Mapeamento dos Relacionamentos



            builder.Property(o => o.Cod_MesReferencia);
            builder.HasOne(o => o.MesReferencia).WithMany().HasForeignKey(o=> o.Cod_MesReferencia);

            builder.Property(c => c.Cod_Cliente);
            builder.HasMany(c => c.Clientes).WithOne().HasForeignKey(c => c.Cod_Cliente);


            builder.Property(o => o.Cod_Material);
            builder.HasOne(o => o.Material).WithMany().HasForeignKey(o => o.Cod_Material);

            builder.Property(o => o.Cod_Motorista);
            builder.HasOne(o => o.Motorista).WithMany().HasForeignKey(o => o.Cod_Motorista);


            #endregion


        }

    }
}

