using Projeto.Data.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Projeto.Data.Entities
{
   public class Rota
    {

        private Rota()
        {
        }

        public Rota(int cod_Rota, string nome)
        {
            this.Cod_Rota = cod_Rota;
            this.Nome = nome;
        }

        public static Rota Criar(
            DataString nome,
            DataString? composicao_rota,
            DataString? observacao,
            bool flag_ativo )
        {
            var rota = new Rota()
            {
                Nome = nome,
                Composicao_Rota = composicao_rota,
                Flag_Ativo = flag_ativo,
                Observacao = observacao   

            };
            //rota.Validate();
            return rota;
        }

        public void Atualizar(DataString nome,
            DataString? composicao_rota,
            DataString? observacao,
            bool flag_ativo)
        {
            {
                Nome = nome;
                Composicao_Rota = composicao_rota;
                Flag_Ativo = flag_ativo;
                Observacao = observacao;
            };
            //Validate();          
        }

        public int Cod_Rota { get; set; }
        public string Nome { get; set; }
        public string Composicao_Rota { get; set; }
        public bool Flag_Ativo { get; set; }
        public string Observacao { get; set; }


    }
}
