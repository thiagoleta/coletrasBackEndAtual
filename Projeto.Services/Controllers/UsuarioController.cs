using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto.Data.Contracts;
using Projeto.Data.Entities;
using Projeto.Services.Models.Usuario;
using Projeto.Services.Util;

namespace Projeto.Services.Controllers
{
    [Authorize("Bearer")]
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        //atributo
        private readonly IUsuarioRepository usuarioRepository;
        private readonly IMapper mapper;

        public UsuarioController(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            this.usuarioRepository = usuarioRepository;
            this.mapper = mapper;
        }
        
        [HttpPost]
        public IActionResult Post(UsuarioCadastroModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //verificar se o email informado ja existe no banco de dados
                    if (usuarioRepository.Obter(model.Email) != null)
                    {
                        return BadRequest("O email informado já encontra-se cadastrado.");
                    }


                    var usuario = new Usuario();
                    usuario.Nome = model.Nome;
                    usuario.Email = model.Email;
                    usuario.Senha = Criptografia.MD5Encrypt(model.Senha);
                    usuario.DataCriacao = DateTime.Now;

                    usuarioRepository.Inserir(usuario);

                    var result = new
                    {
                        mensagem = "Usuário cadastrado com sucesso."
                    };

                    return Ok(result);
                }
                catch (Exception e)
                {
                    return StatusCode(500, "Ocorreu um erro: " + e.Message);
                }
            }
            else
            {
                return BadRequest("Ocorreram erros de validação.");
            }
        }


        [HttpPut]
        public IActionResult Put(UsuarioEdicaoModel model)
        {
            //verificando se os campos da model passaram nas validações
            if (ModelState.IsValid)
            {
                try
                {

                    var usuario = new Usuario();
                    usuario.Nome = model.Nome;
                    usuario.Email = model.Email;
                    usuario.Senha = Criptografia.MD5Encrypt(model.Senha);                    
                    usuarioRepository.Atualizar(usuario);

                    var result = new
                    {
                        message = "Usuário atualizado com sucesso",
                        usuario
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


        [HttpGet]
        [Produces(typeof(List<UsuarioConsultaModel>))]
        public IActionResult GetAll()
        {
            try
            {
                var result = usuarioRepository.ConsultarTodos();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro: " + e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            try
            {
                //buscar o Rota referente ao id informado..
                var usuario = usuarioRepository.ObterPorId(id);

                //verificar se o Rota foi encontrado..
                if (usuario != null)
                {
                    //excluindo o Rota
                    usuarioRepository.Excluir(usuario);

                    var result = new
                    {
                        message = "Usuário excluído com sucesso.",
                        usuario
                    };

                    return Ok(result);
                }
                else
                {
                    return BadRequest("Uasuário não encontrado.");
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, "Erro: " + e.Message);
            }
        }


        [HttpGet("{id}")]
        [Produces(typeof(UsuarioConsultaModel))]
        public IActionResult GetById(int id)
        {
            try
            {
                var result = usuarioRepository.ObterPorId(id);

                if (result != null) 
                {
                    return Ok(result);
                }
                else
                {
                    return NoContent();
                }
            }

            catch (Exception e)
            {
                return StatusCode(500, "Erro: " + e.Message);
            }
        }
    }
}
