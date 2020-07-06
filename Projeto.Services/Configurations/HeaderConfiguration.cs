using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Services.Configurations
{
    public class HeaderConfiguration : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            //configurando o swagger para permitir que seja passado um TOKEN
            //no cabeçalho das requisições de cada serviço da API
            if (operation == null)
                operation.Parameters = new List<IParameter>();

            operation.Parameters.Add(new NonBodyParameter
            {
                Name = "Authorization",
                In = "header",
                Type = "string",
                Required = false
            });
        }

    }
}
