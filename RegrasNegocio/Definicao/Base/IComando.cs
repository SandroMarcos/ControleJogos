using System.Threading.Tasks;

namespace RegrasNegocio
{
    public interface IComando<TEntrada, TSaida>
    {
        int IdUsuario { set; }
        Task<TSaida> Executar(TEntrada entrada);
    }

    public interface IComando<TSaida>
    {
        int IdUsuario { set; }
        Task<TSaida> Executar();
    }
}
