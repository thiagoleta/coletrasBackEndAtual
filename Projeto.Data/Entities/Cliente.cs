using Projeto.Data.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Projeto.Data.Entities
{
   public class Cliente
    {

        public Cliente()
        {

        }

        public Cliente(int cod_cliente, string nomeCliente)
        {
            this.Cod_Cliente = cod_cliente;
            this.NomeCompleto_RazaoSocial = nomeCliente;
        }


        public void Atualizar(
            DataString cPF_CNPJ,
            DataString nomeCompleto_RazaoSocial,
            DataString fantasia,
            DataString? insc_Estadual,
            DataString? logradouro,
            DataString? endereco,
            DataString? bairro,
            DataString? complemento,
            DataString? cidade,
            DataString? cEP,
            DataString? uF,
            DataString? telefones,
            DataString? funcao,            
            bool? flag_Ativo,
            string email,
            DataString? observacao,
            DataString? referencia)
        {            
            CPF_CNPJ = cPF_CNPJ;
            NomeCompleto_RazaoSocial = nomeCompleto_RazaoSocial;
            Fantasia = fantasia;
            Insc_Estadual = insc_Estadual;
            Logradouro = logradouro;
            Endereco = endereco;
            Bairro = bairro;
            Complemento = complemento;
            Cidade = cidade;
            CEP = cEP;
            UF = uF;
            Telefones = telefones;
            Funcao = funcao;
            Email = email;
            Flag_Ativo = flag_Ativo;
            Observacao = observacao;
            Referencia = referencia;

            //Validate();
        }

        public static Cliente Criar(
             DataString cPF_CNPJ,
             DataString nomeCompleto_RazaoSocial,
             DataString fantasia,
             DataString? insc_Estadual,
             DataString? logradouro,
             DataString? endereco,
             DataString? bairro,
             DataString? complemento,
             DataString? cidade,
             DataString? cEP,
             DataString? uF,
             DataString? telefones,
             DataString? funcao,
             bool? flag_Ativo,
             string email,
             DataString? observacao,
             DataString? referencia)
         
        {
            var cliente = new Cliente()
            {
                CPF_CNPJ = cPF_CNPJ,
                NomeCompleto_RazaoSocial = nomeCompleto_RazaoSocial,                
                Fantasia = fantasia,
                Insc_Estadual = insc_Estadual,
                Logradouro = logradouro,                
                Endereco = endereco,
                Bairro = bairro,
                Complemento = complemento,
                Cidade = cidade,
                CEP = cEP,
                UF = uF,
                Telefones = telefones,
                Funcao = funcao,
                Flag_Ativo = flag_Ativo,
                Email = email,
                Observacao = observacao,
                Referencia = referencia,
                
            };
            //cliente.Validate();
            return cliente;
        }

      

        public int Cod_Cliente { get; set; }
        public string CPF_CNPJ { get; set; }
        public string NomeCompleto_RazaoSocial { get; set; }
        public string Fantasia { get; set; }
        public string Insc_Estadual { get; set; }
        public string Logradouro { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
		public string Cidade { get; set; }
        public string CEP { get; set; }
        public string UF { get; set; }
        public string Telefones { get; set; }
        public string Funcao { get; set; }
        public string Email { get; set; }
        public bool? Flag_Ativo { get; set; }
        public string Observacao { get; set; }
        public string Referencia { get; set; }

    }
}
