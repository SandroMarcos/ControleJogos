using AcessoADados;
using AutoMapper;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repositorios
{
    public class RepositorioBase<T> : IRepositorioBase<T> where T : class
    {
        protected readonly IRepositorio repositorio;
        protected readonly IMapper mapper;

        public RepositorioBase(IRepositorio _repositorio, IMapper _mapper)
        {
            repositorio = _repositorio;
            mapper = _mapper;
        }

        protected IQueryable<T> Consultar() => repositorio.Consultar<T>();

        protected IQueryable<T> Consultar(Expression<Func<T, bool>> expressao) => repositorio.Consultar<T>(expressao);
        
        public async Task Alterar(T obj) => await repositorio.Alterar(obj);

        public async Task<int> Count() => await repositorio.Count<T>();

        public async Task<int> Count(Expression<Func<T, bool>> expressao) => await repositorio.Count(expressao);

        public async Task Excluir(T obj) => await repositorio.Excluir(obj);

        public async Task Gravar(T obj) => await repositorio.Gravar(obj);

        public async Task<object> Inserir(T obj) => await repositorio.Inserir(obj);

        public async Task<T> ObterPorId(int id) => await repositorio.ObterPorId<T>(id);

        public async Task<bool> Existe(Expression<Func<T, bool>> expressao) => await repositorio.Existe<T>(expressao);
    }
}
