using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repositorios
{
    public interface IRepositorioBase<T> where T : class
    {
        Task<object> Inserir(T obj);
        Task Alterar(T obj);
        Task Gravar(T obj);
        Task Excluir(T obj);
        Task<T> ObterPorId(int id);
        Task<int> Count();
        Task<int> Count(Expression<Func<T, bool>> expressao);
        Task<bool> Existe(Expression<Func<T, bool>> expressao);

    }
}
