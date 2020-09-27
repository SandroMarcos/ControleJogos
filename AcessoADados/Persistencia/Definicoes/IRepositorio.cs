using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AcessoADados
{
    public interface IRepositorio
    {        
        Task<object> Inserir<T>(T obj);
        Task Alterar<T>(T obj);
        Task Gravar<T>(T obj);
        Task Excluir<T>(T obj);
        Task<T> ObterPorId<T>(int id);
        IQueryable<T> Consultar<T>();
        IQueryable<T> Consultar<T>(Expression<Func<T, bool>> expressao);
        Task<int> Count<T>();
        Task<int> Count<T>(Expression<Func<T, bool>> expressao);
        Task<bool> Existe<T>(Expression<Func<T, bool>> expressao);
    }
}
