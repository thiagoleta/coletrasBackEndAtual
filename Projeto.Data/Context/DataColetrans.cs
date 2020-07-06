using Microsoft.EntityFrameworkCore;
using Projeto.Data.Entities;
using Projeto.Data.Mappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Context
{
    //REGRA 1) DeverA? HERDAR DbContext
    public class DataColetrans : DbContext
    {


        //REGRA 2) Criando um construtor para injeA你υ de dependA?ncia
        //este construtor irA? receber configuraA你畫s definidas na
        //classe Startup.cs do projeto API
        public DataColetrans(DbContextOptions<DataColetrans> options)
            : base(options) //construtor da superclasse
        {

        }

        //REGRA 3) Sobrescrita (OVERRIDE) do mA孤odo OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //adicionar cada classe de mapeamento (Mapping) feito no projeto
	
            modelBuilder.ApplyConfiguration(new ClienteMapping());
            modelBuilder.ApplyConfiguration(new ConfigucacaoMapping());
            modelBuilder.ApplyConfiguration(new ContratoMapping());
            modelBuilder.ApplyConfiguration(new MaterialMapping());
            modelBuilder.ApplyConfiguration(new MesReferenciaMapping());
            modelBuilder.ApplyConfiguration(new MotoristaMapping());
            modelBuilder.ApplyConfiguration(new OSMapping());
            modelBuilder.ApplyConfiguration(new PagamentoMapping());
            modelBuilder.ApplyConfiguration(new RotaMapping());


        }

        //REGRA 4) Declarar um set/get utilizando a classe DbSet do EF
        //para cada entidade do projeto. Este DbSet irA? permitir o uso
        //de expressA畫s LAMBDA para executar consultas com qualquer
        //uma das entidades

        public DbSet<Cliente> Cliente { get; set; } //LAMBDA Functions
        public DbSet<Configucacao> Configucacao { get; set; } //LAMBDA Functions
        public DbSet<Contrato> Contrato { get; set; } //LAMBDA Functions
        public DbSet<Material> Material { get; set; } //LAMBDA Functions
        public DbSet<MesReferencia> MesReferencia { get; set; } //LAMBDA Functions
        public DbSet<Motorista> Motorista { get; set; } //LAMBDA Functions
        public DbSet<OS> OS { get; set; } //LAMBDA Functions
        public DbSet<Pagamento> Pagamento { get; set; } //LAMBDA Functions
        public DbSet<Rota> Rota { get; set; } //LAMBDA Functions

    }

}

