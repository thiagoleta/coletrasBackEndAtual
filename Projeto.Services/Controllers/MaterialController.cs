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
using Projeto.Services.Models.Material;

namespace Projeto.Services.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialController : ControllerBase
    {
        //atributo
        private readonly IMaterialRepository materialRepository;
        private readonly IMapper mapper;

        public MaterialController(IMaterialRepository materialRepository, IMapper mapper)
        {
            this.materialRepository = materialRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult Post(MaterialCadastroModel model)
        {
            //verificando se os campos da model passaram nas ões
            if (ModelState.IsValid)
            {
                try
                {
                    var material = mapper.Map<Material>(model);
                    materialRepository.Inserir(material);

                    var result = new
                    {
                        message = "Material cadastrada com sucesso", 
                        	material
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
        public IActionResult Put(MaterialEdicaoModel model)
        {
            //verificando se os campos da model passaram nas ões
            if (ModelState.IsValid)
            {
                try
                {
                    var material = mapper.Map<Material>(model);
                    materialRepository.Alterar(material);

                    var result = new
                    {
                        message = "Material atualizado com sucesso",
                        	material
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
                //buscar o Material referente ao id informado..
                var material = materialRepository.ObterPorId(id);

                //verificar se o Material foi encontrado..
                if (material != null)
                {
                    //excluindo o Material
                    materialRepository.Excluir(material);

                    var result = new
                    {
                        message = "Material excluído com sucesso.",
                        	material
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
        [Produces(typeof(List<MaterialConsultaModel>))]
        public IActionResult GetAll()
        {
            try
            {
                var result = materialRepository.Consultar();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro: " + e.Message);
            }
        }

        [HttpGet("{id}")]
        [Produces(typeof(MaterialConsultaModel))]
        public IActionResult GetById(int id)
        {
            try
            {
                var result = materialRepository.ObterPorId(id);

                if (result != null) //se o Material foi encontrado..
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
