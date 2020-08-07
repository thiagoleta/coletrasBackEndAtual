using AutoMapper;
using Projeto.Data.Entities;
using Projeto.Services.Models.Motorista;
using Projeto.Services.Models.Rota;
using Projeto.Services.Models.Usuario;
using Projeto.Services.Models.Cliente;
using Projeto.Services.Models.Configucacao;
using Projeto.Services.Models.Contrato;
using Projeto.Services.Models.Material;
using Projeto.Services.Models.MesReferencia;
using Projeto.Services.Models.OS;
using Projeto.Services.Models.Pagamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projeto.Services.Models.Roteiro;

namespace Projeto.Services.Mappings
{
    public class EntityToViewModelMap : Profile
    {
        public EntityToViewModelMap()
        {
            #region Motorista
            CreateMap<Motorista, MotoristaConsultaModel>();
            #endregion

            #region Rota
            CreateMap<Rota, RotaConsultaModel>();
            #endregion

            #region Cliente
            CreateMap<Cliente, ClienteConsultaModel>();
            #endregion

            #region Configucacao
            CreateMap<Configucacao, ConfigucacaoConsultaModel>();
            #endregion

            #region Contrato
            CreateMap<Contrato, ContratoConsultaModel>();
            #endregion

            #region Material
            CreateMap<Material, MaterialConsultaModel>();
            #endregion

            #region MesReferencia
            CreateMap<MesReferencia, MesReferenciaConsultaModel>();
            #endregion

            #region OS
            CreateMap<OS, OSConsultaModel>();
            #endregion

            #region Pagamento
            CreateMap<Pagamento, PagamentoConsultaModel>();
            #endregion

            



            #region Usuario
            CreateMap<Usuario, UsuarioCadastroModel>();
            #endregion

            #region Roteiro
            CreateMap<Roteiro, RoteiroCadastroModel>();
            #endregion


        }

    }
}
