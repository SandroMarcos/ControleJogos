using AcessoADados;
using Modelos;
using System.Threading.Tasks;

namespace RegrasNegocio
{
    public interface IConsultaUsuario : IComando<string, Usuario>
    {
        Task<Usuario> Executar(int id);        
    }
}
