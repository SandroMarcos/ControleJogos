using AcessoADados;
using Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositorios
{
    public interface IRepositorioJogo : IRepositorioCrud<Jogo>
    {
        Task<IList<JogosDisponiveisModel>> ObterJogosDisponiveis();

        Task<IList<JogoReduzidoModel>> ObterMeusJogos();
    }
}
