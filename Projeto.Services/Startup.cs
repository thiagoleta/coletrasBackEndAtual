using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Projeto.Data.Context;
using Projeto.Data.Contracts;
using Projeto.Data.Repository;
using Projeto.Data.Services;
using Projeto.Data.Repository.RepositoriosDapper;
using Projeto.Services.Configurations;
using Projeto.Services.Util;
using Swashbuckle.AspNetCore.Swagger;
using Projeto.Data.Services.Implementations;
using Projeto.Data.Seedwork.Notifying;

namespace Projeto.Services
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            #region AutoMapper
            services.AddAutoMapper(typeof(Startup));
            #endregion

            #region EntityFramework

            services.AddDbContext<DataColetrans>
                 (options => options.UseSqlServer(Configuration.GetConnectionString("Coletrans")));

            #region Criptografias e Serviços de Email
            services.AddTransient<Criptografia>();
            services.AddTransient<SHA1Encrypt>();

            var mailSettings = new MailSettings();
            new ConfigureFromConfigurationOptions<MailSettings>
                (Configuration.GetSection("MailSettings"))
                .Configure(mailSettings);

            services.AddTransient<MailService>
                (map => new MailService(mailSettings));

            #endregion



            #region Injeção de Dependência EntityFramework

            services.AddTransient<IMotoristaRepository, MotoristaRepository>();
            services.AddScoped<IMotoristaService, MotoristaService>();
            services.AddTransient<IRotaRepository, RotaRepository>();
            services.AddTransient<IRotaService, RotaService>();
            services.AddTransient<IClienteRepository, ClienteRepository>();
            services.AddTransient<IClienteService, ClienteService>();
            services.AddTransient<IConfiguracaoRepository, ConfiguracaoRepository>();            
            services.AddTransient<IContratoRepository, ContratoRepository>();
            services.AddTransient<IContratoService, ContratoService>();
            services.AddTransient<IMaterialRepository, MaterialRepository>();
            services.AddScoped<IMaterialService, MaterialService>();
            services.AddTransient<IMesReferenciaRepository, MesReferenciaRepository>();
            services.AddTransient<IMesReferenciaService, MesReferenciaService>();
            services.AddTransient<IOSRepository, OSRepository>();
            services.AddTransient<IOsServices, OsService>();
            services.AddTransient<IPagamentoRepository, PagamentoRepository>();
            services.AddTransient<IPagamentoService, PagamentoService>();
            services.AddTransient<IDiasColetaRepository, DiasColetaRepository>();
            services.AddTransient<IPerfilRepository, PerfilRepository>();
            services.AddTransient<IPerfilService, PerfilService>();
            services.AddTransient<IRoteiroService, RoteiroService>();
            services.AddTransient<IRoteiroRepository, RoteiroRepository>();
            services.AddTransient<ITurnoRepository, TurnoRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IUsuarioService, UsuarioService>();



            #endregion

            #endregion

            #region Dapper config e Injeção de dependência

            var connectionString = Configuration.GetConnectionString("Coletrans");

          //  mapear as interfaces e classe criadas no repositorio


            //services.AddTransient<IUsuarioRepository, UsuarioRepository>
            //   (map => new UsuarioRepository(connectionString));
            #endregion


            #region Swagger

            services.AddSwaggerGen(
               c =>
               {
                   c.SwaggerDoc("v1", new Info                   {
                       Title = "Sistema de Controle Coleta de Lixo",
                       Description = "API REST para integração com serviços de coleta de lixo",
                        Version = "v1",
                       Contact = new Contact
                       {
                           Name = "Thiago Leta",
                           Url = "Em construção",
                           Email = "thiagoleta2013@gmail.com"
                       }
                   });

                   //registrando a configuração para uso do HEADER no swagger
                   c.OperationFilter<HeaderConfiguration>();
               }
                );



            #endregion        

            #region Token
            //mapeando injeção de dependência para a classe tokenconfiguration
            var tokenConfiguration = new TokenConfiguration();
            new ConfigureFromConfigurationOptions<TokenConfiguration>
                (Configuration.GetSection("TokenConfiguration"))
                .Configure(tokenConfiguration);

            //singleton -> terá uma unica instancia durante 
            //toda a execução do projeto

            services.AddSingleton(tokenConfiguration);

            //mapear injeção de dependencia login configuration
            var loginConfiguration = new LoginConfiguration();
            services.AddSingleton(loginConfiguration);

            //executando a configuração do JWT para autenticação
            JwtConfiguration.Register(services, tokenConfiguration, loginConfiguration);

            #endregion

            #region Cors
            services.AddRouting(o => o.LowercaseUrls = true);
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder => {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("*");
            }));

        }
        #endregion





        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var supportedCultures = new[] { new CultureInfo("pt-BR") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }     
       

            #region Swagger

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Projeto API");
            });

            #endregion


            app.UseMvc();
        }
    }
}
