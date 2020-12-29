using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Projeto.Data.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Services.Controllers
{/// <summary>
 /// Controller base da api
 /// </summary>
    public abstract class ApiControllerBase : ControllerBase
    {
        /// <summary>
        /// Retorna um Result de acordo com o CommandResult fornecido
        /// </summary>
        /// <param name="result"></param>
        /// <returns>Result</returns>
        // CommandResult<T> : CommandResult
        public IActionResult Result(CommandResult result)
        {
            switch (result.Tipo)
            {
                case ResultType.Valid:
                    return Ok();

                case ResultType.Forbidden:
                    return Forbid();

                case ResultType.Invalid:
                default:
                    return BadRequest(result.Mensagens);
            }
        }

        /// <summary>
        /// Retorna um Result de tipo genérico de acordo com o CommandResult fornecido
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <returns>Result de tipo genérico</returns>
        public IActionResult Result<T>(CommandResult<T> result)
        {
            switch (result.Tipo)
            {
                case ResultType.Valid:
                    return Ok(result.Dados);

                case ResultType.Forbidden:
                    return Forbid();

                case ResultType.Invalid:
                default:
                    return BadRequest(result.Mensagens);
            }
        }

        /// <summary>
        /// Obtém valores em classes da QueryString do Request
        /// </summary>
        /// <typeparam name="T">Tipo da Classe</typeparam>
        /// <param name="querystring">Nome da Querystring</param>
        /// <returns></returns>
        [Obsolete]
        protected T ObterVariavelQueryString<T>(string querystring)
        {
            T query = default(T);
            var obteveValor = HttpContext.Request.Query.TryGetValue(querystring, out StringValues outQuerystring);
            if (obteveValor) query = JsonConvert.DeserializeObject<T>(outQuerystring[0]);
            return query;
        }
    }
}