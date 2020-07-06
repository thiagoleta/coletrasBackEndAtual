using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Services.Models.Configucacao
{
    public class ConfigucacaoEdicaoModel
    {

        //region PrimareKey
        public int Cod_Configucacao { get; set; }

        //region KeyColumns
        public string Empresa { get; set; }
        public string Numero_Inea { get; set; }
        public string Logradouro { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string CEP { get; set; }
        public string UF { get; set; }
        public string Telefones { get; set; }

        //region Associacao RefForeignKey

    }
}
