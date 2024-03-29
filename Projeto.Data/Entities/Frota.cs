﻿using Projeto.Data.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Entities
{
    public class Frota
    {
        public Frota()
        {

        }
        public Frota(int cod_Frota, string placa)
        {
            this.Cod_Frota = cod_Frota;
            this.Placa = placa;
        }

        public static Frota Criar(Motorista motorista,
                                    DataString descricao,
                                    DataString placa,
                                    DataString? observacao,
                                     DataString? quilometragem)

        {
            var frota = new Frota()
            {
                Cod_Motorista = motorista.Cod_Motorista,
                Descricao = descricao,
                Placa = placa,
                Observacao = observacao,
                Quilometragem = quilometragem
            };
            return frota;
        }

        public void Atualizar(Motorista motorista,
                                DataString descricao,
                                DataString placa,
                                 DataString? observacao,
                                  DataString? quilometragem)
        {
            Cod_Motorista = motorista.Cod_Motorista;
            Descricao = descricao;
            Placa = placa;
            Observacao = observacao;
            Quilometragem = quilometragem;


        }

        public int Cod_Frota { get; set; }
        public int Cod_Motorista { get; set; }
        public string Descricao { get; set; }
        public string Placa { get; set; }
        public string Observacao { get; set; }
        public string Quilometragem { get; set; }

        public Motorista Motorista { get; set; }

    }
}
