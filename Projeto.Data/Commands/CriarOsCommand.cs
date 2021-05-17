using Projeto.Data.Commands.DTO;
using Projeto.Data.Seedwork.Notifying;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Commands
{
   public class CriarOsCommand : Validatable, IValidatable
    {
        public IEnumerable<ClienteDTO> Clientes { get; set; } = Array.Empty<ClienteDTO>();

        public override void Validate()
        {
            if (Clientes == null)
            {
                AddNotification(nameof(Clientes), "Selecione pelo menos um Cliente.");
            }
        }
    }
}
