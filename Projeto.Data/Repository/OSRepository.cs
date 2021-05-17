using Microsoft.EntityFrameworkCore;
using Projeto.Data.Context;
using Projeto.Data.Contracts;
using Projeto.Data.Entities;
using Projeto.Data.Extensions;
using Projeto.Data.Repository.Sorts;
using Projeto.Data.Seedwork;
using Projeto.Data.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projeto.Data.Repository
{
   public class OSRepository :  IOSRepository
    {
        private readonly DataColetrans dataContext;

        public OSRepository(DataColetrans dataContext)
        {
            this.dataContext = dataContext;
        } 

        public CommandResult<IReadOnlyCollection<OS>> Obter(OsSort sort, bool ascending, DataString? nomeCompleto_RazaoSocial, DataString? mesAno)
        {
            var resultado = ObterBase(sort, ascending, nomeCompleto_RazaoSocial, mesAno).ToArray();
            return CommandResult<IReadOnlyCollection<OS>>.Valid(resultado);
        }


        public CommandResult<PaginatedQueryResult<OS>> ObterPaginado(OsSort sort, bool ascending, int pagina, int quantidade, DataString? nomeCompleto_RazaoSocial, DataString? mesAno)
        {
            var listaBase = ObterBase(sort, ascending, nomeCompleto_RazaoSocial, mesAno);
            var total = listaBase.Count();
            var skip = Pagination.PagesToSkip(quantidade, total, pagina);

            var resultado = new PaginatedQueryResult<OS>()
            {
                Total = total,
                Data = listaBase.Skip(skip).Take(quantidade).ToArray()
            };

            return CommandResult<PaginatedQueryResult<OS>>.Valid(resultado);
        }


        private IQueryable<OS> ObterBase(OsSort sort, bool ascending, DataString? nomeCompleto_RazaoSocial, DataString? mesAno)

        {

            IQueryable<OS> query = dataContext.OS.Include(c => c.Cliente)
                                                           .Include(c => c.MesReferencia)                                                                                                                       
                                                            .Include(c => c.Material)
                                                            .Include(c => c.Motorista);

            switch (sort)
            {

                case OsSort.Cod_OS:
                    if (ascending)
                    {
                        query = query.OrderBy(a => a.Cod_OS.Equals(null) ? 0 : 1).ThenBy(a => a.Cod_OS);
                    }
                    else
                    {
                        query = query.OrderByDescending(a => a.Cod_OS.Equals(null) ? 0 : 1).ThenByDescending(a => a.Cod_OS);
                    }
                    break;
                case OsSort.Data_Geracao:
                    {
                        query = ascending ? query.OrderBy(x => x.Data_Geracao == null ? 0 : 1).ThenBy(x => x.Data_Geracao) :
                        query = query.OrderByDescending(x => x.Data_Geracao == null ? 0 : 1).ThenByDescending(x => x.Data_Geracao);
                        break;
                    }
                case OsSort.MesAno:
                    {
                        query = ascending ? query.OrderBy(x => x.MesReferencia.MesAno == null ? 0 : 1).ThenBy(x => x.MesReferencia.MesAno) :
                        query = query.OrderByDescending(x => x.MesReferencia.MesAno == null ? 0 : 1).ThenByDescending(x => x.MesReferencia.MesAno);
                        break;
                    }          
                case OsSort.NomeMotorista:
                    {
                        query = ascending ? query.OrderBy(x => x.Motorista.Nome == null ? 0 : 1).ThenBy(x => x.Motorista.Nome) :
                        query = query.OrderByDescending(x => x.Motorista.Nome == null ? 0 : 1).ThenByDescending(x => x.Motorista.Nome);
                        break;
                    }
                case OsSort.Quantidade_Coletada:
                    {
                        query = ascending ? query.OrderBy(x => x.Quantidade_Coletada == null ? 0 : 1).ThenBy(x => x.Quantidade_Coletada) :
                        query = query.OrderByDescending(x => x.Quantidade_Coletada == null ? 0 : 1).ThenByDescending(x => x.Quantidade_Coletada);
                        break;
                    }
                case OsSort.Data_Coleta:
                    {
                        query = ascending ? query.OrderBy(x => x.Data_Coleta == null ? 0 : 1).ThenBy(x => x.Data_Coleta) :
                        query = query.OrderByDescending(x => x.Data_Coleta == null ? 0 : 1).ThenByDescending(x => x.Data_Coleta);
                        break;
                    }                
                case OsSort.Flag_Coleta:
                    {
                        query = ascending ? query.OrderBy(x => x.Flag_Coleta == null ? 0 : 1).ThenBy(x => x.Flag_Coleta) :
                        query = query.OrderByDescending(x => x.Flag_Coleta == null ? 0 : 1).ThenByDescending(x => x.Flag_Coleta);
                        break;
                    }
                case OsSort.Hora_Entrada:
                    {
                        query = ascending ? query.OrderBy(x => x.Hora_Entrada == null ? 0 : 1).ThenBy(x => x.Hora_Entrada) :
                        query = query.OrderByDescending(x => x.Hora_Entrada == null ? 0 : 1).ThenByDescending(x => x.Hora_Entrada);
                        break;
                    }
                case OsSort.Hora_Saida:
                    {
                        query = ascending ? query.OrderBy(x => x.Hora_Saida == null ? 0 : 1).ThenBy(x => x.Hora_Saida) :
                        query = query.OrderByDescending(x => x.Hora_Saida == null ? 0 : 1).ThenByDescending(x => x.Hora_Saida);
                        break;
                    }
                case OsSort.Placa:
                    {
                        query = ascending ? query.OrderBy(x => x.Placa == null ? 0 : 1).ThenBy(x => x.Placa) :
                        query = query.OrderByDescending(x => x.Placa == null ? 0 : 1).ThenByDescending(x => x.Placa);
                        break;
                    }
                case OsSort.Flag_Cancelado:
                    {
                        query = ascending ? query.OrderBy(x => x.Flag_Cancelado == null ? 0 : 1).ThenBy(x => x.Flag_Cancelado) :
                        query = query.OrderByDescending(x => x.Flag_Cancelado == null ? 0 : 1).ThenByDescending(x => x.Flag_Cancelado);
                        break;
                    }
                default:
                    query = ascending ? query.OrderBy(x => x.Cliente.NomeCompleto_RazaoSocial == null ? 0 : 1).ThenBy(x => x.Cliente.NomeCompleto_RazaoSocial) :
                    query = query.OrderByDescending(x => x.Cliente.NomeCompleto_RazaoSocial == null ? 0 : 1).ThenByDescending(x => x.Cliente.NomeCompleto_RazaoSocial);
                    break;
            }
            return query
                 .AsNoTracking()
                .WhereIfNotNull(x => x.Cliente.NomeCompleto_RazaoSocial.ToUpper().Contains(nomeCompleto_RazaoSocial.ToString()), nomeCompleto_RazaoSocial)
                .WhereIfNotNull(x => x.MesReferencia.MesAno.ToUpper().Contains(mesAno.ToString()), mesAno);
        }

    }
}

