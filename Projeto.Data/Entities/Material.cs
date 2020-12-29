using Projeto.Data.Seedwork;
using Projeto.Data.Seedwork.Notifying;
using Projeto.Data.Validators.PrimitiveValidators;
using Projeto.Data.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Projeto.Data.Entities
{
   public sealed class Material /*: Notifiable, IEntity, INotifiable*/

    {

		private Material()
		{
		}


        public static Material Criar(DataString descricao, DataString? volume, DataString? observacao, DataString? material_coletado)
        {
            var material = new Material()
            {
                Descricao = descricao,
                Volume = volume,
                Observacao = observacao,
                Material_Coletado = material_coletado,             


            };
            material.Validate();
            return material;
        }

        public void Atualizar(DataString descricao, string volume, DataString observacao, DataString material_coletado)
        {
            Descricao = descricao;
            Volume = volume;
            Observacao = observacao;
            Material_Coletado = material_coletado;            

            Validate();
        }

         
        public int Cod_Material { get; private set; }
        public string Descricao { get; private set; }
        public string Volume { get; private set; }
        public string Observacao { get; private set; }
        public string Material_Coletado { get; private set; }

        public void Validate()
        {
            //if (!Descricao.HasMaxLength(400))
            //{
            //    AddNotification(nameof(Descricao), "O Nome permite no máximo 200 caracteres.");
            //}

        }

    }

}
