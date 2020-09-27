namespace AcessoADados
{
    using NHibernate;
    using NHibernate.Linq;
    using NHibernate.Persister.Entity;
    using System;
    using System.Collections.Generic;
    using System.Data;    
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    /// Classe <see cref="Repositorio{T}"/>
    /// </summary>
    /// <typeparam name="T">Classe genérica</typeparam>    
    public class Repositorio : IRepositorio 
    {
        /// <summary>
        /// Obter ou definir a unidade de trabalho
        /// </summary>
        private readonly IUnidadeTrabalho unidadeTrabalho;

        public Repositorio() { }

        /// <summary>
        /// Construtor que recebe uma instancia de <see cref="IUnidadeTrabalho"/>.
        /// </summary>
        /// <param name="unidadeTrabalho">Instancia de <see cref="IUnidadeTrabalho"/>.</param>
        public Repositorio(IUnidadeTrabalho unidadeTrabalho)
        {
            this.unidadeTrabalho = unidadeTrabalho;
        }

        //public T Entidade { get; set; }

        /// <summary>
        /// Não é necessário abstrair a tecnologia nessa camada, afinal,
        /// ela já conhece o <see cref="NHibernate"/>. Estamos fazendo o <see cref="Cast"/> para obter a
        /// Sessão e ter todas as suas funcionalidades em nossas mãos.
        /// </summary>
        public virtual ISession Sessao => ((UnidadeTrabalho)unidadeTrabalho).Sessao;

        /// <summary>
        /// Obtém uma instancia fazendo uma busca no banco de dados pelo identificador informado.
        /// </summary>
        /// <param name="id">ID do objeto.</param>
        /// <returns>Objeto tipo T.</returns>
        public virtual async Task<T> ObterPorId<T>(int id) => await Sessao.GetAsync<T>(id);

        /// <summary>
        /// Obtém um objeto de consulta que implementa a interface <see cref="IQueryable"/>
        /// </summary>
        /// <returns>Objeto <see cref="IQueryable"/>.</returns>
        public virtual IQueryable<T> Consultar<T>() => Sessao.Query<T>();

        /// <summary>
        /// Obtém um objeto de consulta que implementa a interface <see cref="IQueryable"/>,
        /// executando um filtro (<see cref="expressao"/>).
        /// </summary>
        /// <param name="expressao">Filtro da busca.</param>
        /// <returns>Objeto <see cref="IQueryable"/>.</returns>        
        public virtual IQueryable<T> Consultar<T>(Expression<Func<T, bool>> expressao) => Sessao.Query<T>().Where(expressao);

        /// <summary>
        /// Insere o objeto em banco de dados.
        /// </summary>
        /// <param name="obj">Objeto persistente.</param>
        public virtual async Task<object> Inserir<T>(T obj) => await Sessao.SaveAsync(obj);

        /// <summary>
        /// Atualiza os dados o objeto no Banco de dados.
        /// </summary>
        /// <param name="obj">Objeto persistente.</param>
        public virtual async Task Alterar<T>(T obj)
        {            
            await Sessao.UpdateAsync(obj);
        }

        /// <summary>
        /// Insere ou altera os dados do objeto em Banco de dados.
        /// </summary>
        /// <param name="obj">Objeto persistente.</param>
        public virtual async Task Gravar<T>(T obj) => await Sessao.SaveOrUpdateAsync(obj);

        /// <summary>
        /// Exclui o objeto do banco de dados.
        /// </summary>
        /// <param name="obj">Objeto persistente.</param>
        public virtual async Task Excluir<T>(T obj) => await Sessao.DeleteAsync(obj);

        /// <summary>
        /// Contador de objetos gravados no Banco de dados.
        /// </summary>
        /// <returns>Quantidade de objetos salvos.</returns>
        public virtual async Task<int> Count<T>()
        {
            IQueryable<T> iq = Sessao.Query<T>();
            int count = await iq.CountAsync();
            return count;
        }

        /// <summary>
        /// Contador de objetos gravados no Banco de dados e filtrados pela expressão.
        /// </summary>
        /// <param name="expressao">Filtro da busca</param>
        /// <returns>Quantidade de objetos filtrados.</returns>
        public virtual async Task<int> Count<T>(Expression<Func<T, bool>> expressao)
        {
            IQueryable<T> iq = Sessao.Query<T>().Where(expressao);
            int count = await iq.CountAsync();
            return count;
        }

        /// <summary>
        /// Avalia se o objeto existe
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expressao"></param>
        /// <returns></returns>
        public virtual async Task<bool> Existe<T>(Expression<Func<T, bool>> expressao)
        {
            return await Sessao.Query<T>().AnyAsync(expressao);
        }
    }
}
