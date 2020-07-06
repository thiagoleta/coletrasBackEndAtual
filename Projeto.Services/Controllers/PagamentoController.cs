using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto.Data.Contracts;
using Projeto.Data.Entities;
using Projeto.Data.Repository;
using Projeto.Services.Models.Pagamento;

namespace Projeto.Services.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class PagamentoController : ControllerBase
    {
        //atributo
        private readonly IPagamentoRepository pagamentoRepository;
        private readonly IMapper mapper;

        public PagamentoController(IPagamentoRepository pagamentoRepository, IMapper mapper)
        {
            this.pagamentoRepository = pagamentoRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult Post(PagamentoCadastroModel model)
        {
            //verificando se os campos da model passaram nas ões
            if (ModelState.IsValid)
            {
                try
                {
                    var pagamento = mapper.Map<Pagamento>(model);
                    pagamentoRepository.Inserir(pagamento);

                    var result = new
                    {
                        message = "Pagamento cadastrada com sucesso", 
                        	pagamento
                    };

                    return Ok(result); //HTTP 200 (SUCESSO!)
                }
                catch (Exception e)
                {
                    return StatusCode(500, "Erro: " + e.Message);
                }
            }
            else
            {
                //Erro HTTP 400 (BAD REQUEST)
                return BadRequest("Ocorreram erros de validação.");
            }
        }

        [HttpPut]
        public IActionResult Put(PagamentoEdicaoModel model)
        {
            //verificando se os campos da model passaram nas ões
            if (ModelState.IsValid)
            {
                try
                {
                    var pagamento = mapper.Map<Pagamento>(model);
                    pagamentoRepository.Alterar(pagamento);

                    var result = new
                    {
                        message = "Pagamento atualizado com sucesso",
                        	pagamento
                    };

                    return Ok(result); //HTTP 200 (SUCESSO!)
                }
                catch (Exception e)
                {
                    return StatusCode(500, "Erro: " + e.Message);
                }
            }
            else
            {
                //Erro HTTP 400 (BAD REQUEST)
                return BadRequest("Ocorreram erros de validação.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            try
            {
                //buscar o Pagamento referente ao id informado..
                var pagamento = pagamentoRepository.ObterPorId(id);

                //verificar se o Pagamento foi encontrado..
                if (pagamento != null)
                {
                    //excluindo o Pagamento
                    pagamentoRepository.Excluir(pagamento);

                    var result = new
                    {
                        message = "Pagamento excluído com sucesso.",
                        	pagamento
                    };

                    return Ok(result);
                }
                else
                {
                    return BadRequest("Estoque não encontrado.");
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro: " + e.Message);
            }
        }

        [HttpGet]
        [Produces(typeof(List<PagamentoConsultaModel>))]
        public IActionResult GetAll()
        {
            try
            {
                var result = pagamentoRepository.Consultar();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro: " + e.Message);
            }
        }

        [HttpGet("{id}")]
        [Produces(typeof(PagamentoConsultaModel))]
        public IActionResult GetById(int id)
        {
            try
            {
                var result = pagamentoRepository.ObterPorId(id);

                if (result != null) //se o Pagamento foi encontrado..
                {
                    return Ok(result);
                }
                else
                {
                    return NoContent(); //HTTP 204 (SUCESSO -> Vazio)
                }
            }

            catch (Exception e)
            {
                return StatusCode(500, "Erro: " + e.Message);
            }
        }
    }
}
