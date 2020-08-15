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
    //classe de mapeamento do AutoMapper de forma a permitir
    //que classes de modelo (Models) possam transferir seus dados
    //para classes de entidade (Entities)

    public class ModelToEntityMapping : Profile
    {
        public ModelToEntityMapping()
        {
            CreateMap<RotaCadastroModel, Rota>();
            CreateMap<RotaEdicaoModel, Rota>();

            CreateMap<MotoristaCadastroModel, Motorista>();            
            CreateMap<MotoristaEdicaoModel, Motorista>();

            CreateMap<ClienteCadastroModel, Cliente>();
            CreateMap<ClienteEdicaoModel, Cliente>();

            CreateMap<ConfiguracaoCadastroModel, Configuracao>();
            CreateMap<ConfiguracaoEdicaoModel, Configuracao>();

            CreateMap<ContratoCadastroModel, Contrato>();
            CreateMap<ContratoEdicaoModel, Contrato>();

            CreateMap<MaterialCadastroModel, Material>();
            CreateMap<MaterialEdicaoModel, Material>();

            CreateMap<MesReferenciaCadastroModel, MesReferencia>();
            CreateMap<MesReferenciaEdicaoModel, MesReferencia>();

            CreateMap<OSCadastroModel, OS>();
            CreateMap<OSEdicaoModel, OS>();

            CreateMap<PagamentoCadastroModel, Pagamento>();
            CreateMap<PagamentoEdicaoModel, Pagamento>();
            

            CreateMap<UsuarioEdicaoModel, UsuarioCadastroModel>();

            CreateMap<RoteiroCadastroModel, Roteiro>();
            CreateMap<RoteiroEdicaoModel, Roteiro>();
        }
    }
}
