using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Services.Configurations
{
    public class JwtConfiguration
    {
        //método para configurar o modo de autenticação do projeto (Bearer)
        //e também o framework utilizado na autenticação (JWT)
        public static void Register(IServiceCollection services,
            TokenConfiguration tokenConfiguration,
            LoginConfiguration loginConfiguration)
        {
            //definindo a politica de autenticação do projeto (Bearer)
            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme =JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme =JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = loginConfiguration.Key;
                paramsValidation.ValidAudience = tokenConfiguration.Audience;
                paramsValidation.ValidIssuer = tokenConfiguration.Issuer;

                //aplicação sempre deverá validar os tokens recebidos
                paramsValidation.ValidateIssuerSigningKey = true;
                //aplicação sempre deverá checar o tempo 
                //de expiração do token recebido
                paramsValidation.ValidateLifetime = true;
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            //finalizando ativando o uso de token para autorização de acesso
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder().AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });
        }
    }
}
