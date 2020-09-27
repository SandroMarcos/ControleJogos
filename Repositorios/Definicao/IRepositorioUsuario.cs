using AcessoADados;
using System.Threading.Tasks;

namespace Repositorios
{
    public interface IRepositorioUsuario : IRepositorioCrud<Usuario>
    {
        Task<Usuario> ObterUsuario(string usuario);
    }
}
