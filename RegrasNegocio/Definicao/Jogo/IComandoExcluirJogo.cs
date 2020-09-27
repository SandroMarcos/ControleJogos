using System.Threading.Tasks;

namespace RegrasNegocio
{
    public interface IComandoExcluirJogo : IComando<int,bool>
    {
        Task<bool> ExcluirJogos(int[] codigos);
    }
}
