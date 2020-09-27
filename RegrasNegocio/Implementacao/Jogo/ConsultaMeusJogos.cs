using Modelos;
using Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RegrasNegocio
{
    public class ConsultaMeusJogos : IConsultaMeusJogos
    {
        private readonly IRepositorioJogo repositorioJogo;

        public int IdUsuario { set => repositorioJogo.IdUsuario = value; }

        public ConsultaMeusJogos(IRepositorioJogo repositorioJogo) => this.repositorioJogo = repositorioJogo;

        public async Task<IList<JogoReduzidoModel>> Executar() => await repositorioJogo.ObterMeusJogos();
    }
}
