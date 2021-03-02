using Projeto.Data.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Entities
{
   public class Roteiro
    {
        public Roteiro()
        {

        }
        public static Roteiro Criar(Cliente cliente,
                                    Turno turno,
                                    Rota rota,
                                    Motorista motorista,
                                    Material material,
                                    bool? segunda,
                                    bool? terca,
                                    bool? quarta,
                                    bool? quinta,
                                    bool? sexta,
                                    bool? sabado,
                                    bool? domingo,
                                    DataString? observacao)
        {
            
            var roteiro = new Roteiro()
            {                 
                Cod_Cliente =cliente.Cod_Cliente,
                Cod_Turno = turno.Cod_Turno,
                Cod_Rota = rota.Cod_Rota,
                Cod_Motorista = motorista.Cod_Motorista,
                Cod_Material= material.Cod_Material,
                Segunda = segunda,
                Terca = terca,
                Quarta = quarta,
                Quinta = quinta,
                Sexta = sexta,
                Sabado = sabado,
                Domingo = domingo,
                Observacao = observacao};            
            return roteiro;
        }


        public void Atualizar(Cliente cliente,
                              Turno turno,
                              Rota rota,
                              Motorista motorista,
                              Material material, 
                              bool? segunda,
                              bool? terca,
                              bool? quarta,
                              bool? quinta,
                              bool? sexta,
                              bool? sabado,
                              bool? domingo,
                              DataString? observacao)
        {
            Cod_Cliente = cliente.Cod_Cliente;
            Cod_Turno = turno.Cod_Turno;
            Cod_Rota = rota.Cod_Rota;
            Cod_Motorista = motorista.Cod_Motorista;
            Cod_Material = material.Cod_Material;
            Segunda = segunda;
            Terca = terca;
            Quarta = quarta;
            Quinta = quinta;
            Sexta = sexta;
            Sabado = sabado;
            Domingo = domingo;
            Observacao = observacao;
        }


        public int Cod_Roteiro { get; set; }
        public int Cod_Cliente { get; set; }
        public int? Cod_Turno { get; set; }        
        public int Cod_Rota { get; set; }
        public int Cod_Motorista { get; set; }
        public int? Cod_Material { get; set; }

        public bool? Segunda { get; set; }
        public bool? Terca { get; set; }
        public bool? Quarta { get; set; }
        public bool? Quinta { get; set; }
        public bool? Sexta { get; set; }
        public bool? Sabado { get; set; }
        public bool? Domingo { get; set; }
        public string Observacao { get; set; }


        public Cliente Cliente { get; set; }
        public Turno Turno { get; set; }        
        public Rota Rota { get; set; }
        public Motorista Motorista { get; set; }
        public Material Material { get; set; }


    }
}
