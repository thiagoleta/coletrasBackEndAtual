using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Projeto.Data.Contracts;
using Projeto.Services.Configurations;
using Projeto.Services.Models.Usuario;
using Projeto.Services.Util;

namespace Projeto.Services.Controllers
{
    [AllowAnonymous]
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        //atributos..
        private readonly IUsuarioRepository usuarioRepository;
        private readonly TokenConfiguration tokenConfiguration;
        private readonly LoginConfiguration loginConfiguration;

        public LoginController(IUsuarioRepository usuarioRepository, TokenConfiguration tokenConfiguration, LoginConfiguration loginConfiguration)
        {
            this.usuarioRepository = usuarioRepository;
            this.tokenConfiguration = tokenConfiguration;
            this.loginConfiguration = loginConfiguration;
        }

        [HttpPost]
        public IActionResult Post(UsuarioLoginModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //buscar no repositorio se existe algum 
                    //usuario com o email e a senha informados..
                    var usuario = usuarioRepository.Obter(model.Email,Criptografia.MD5Encrypt(model.Senha));

                    //verificar se o usuario foi encontrado..
                    if (usuario != null)
                    {
                        try
                        {//criando a identificação do usuário..
                            ClaimsIdentity identity = new ClaimsIdentity(new GenericIdentity(usuario.Email, "Login"),
                                new[]
                                {
                                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString("N")),
                                    new Claim(JwtRegisteredClaimNames.UniqueName,usuario.Email)
                                });

                            //montando o TOKEN..
                            var dataCriacao = DateTime.Now;
                            var dataExpiracao = dataCriacao + TimeSpan.FromSeconds(tokenConfiguration.Seconds);

                            var handler = new JwtSecurityTokenHandler();
                            var securityToken = handler.CreateToken
                                (new SecurityTokenDescriptor{  Issuer = tokenConfiguration.Issuer, Audience = tokenConfiguration.Audience,    SigningCredentials = loginConfiguration.SigningCredentials,Subject = identity,NotBefore = dataCriacao, Expires = dataExpiracao});

                            //gerando o token
                            var token = handler.WriteToken(securityToken);

                            var result = new
                            {
                                mensagem = "Usuário autenticado com sucesso.",token, //TOKEN DE ACESSO!
                                criadoEm = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                                expiraEm = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss")
                            };
                            return Ok(result);
                        }
                        catch (Exception e)
                        {
                            return StatusCode(500, "Erro: " + e.Message);
                        }
                    }
                    else
                    {
                        return Unauthorized(); //acesso negado!
                    }
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
    }
}
