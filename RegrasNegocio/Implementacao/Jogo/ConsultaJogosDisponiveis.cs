using Modelos;
using Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RegrasNegocio
{
    public class ConsultaJogosDisponiveis : IConsultaJogosDisponiveis
    {
        private readonly IRepositorioJogo repositorioJogo;

        public int IdUsuario { set => repositorioJogo.IdUsuario = value ; }

        public ConsultaJogosDisponiveis(IRepositorioJogo repositorioJogo) => this.repositorioJogo = repositorioJogo;

        public async Task<IList<JogosDisponiveisModel>> Executar() => await repositorioJogo.ObterJogosDisponiveis();
    }
}
