using AcessoADados;
using AutoMapper;
using Modelos;
using NHibernate.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositorios
{
    public class RepositorioJogo : RepositorioBase<Jogo>, IRepositorioJogo
    {
        public int IdUsuario { get; set; }

        public RepositorioJogo(IRepositorio _repositorio, IMapper _mapper) : base(_repositorio, _mapper) { }

        public async Task<IList<JogosDisponiveisModel>> ObterJogosDisponiveis()
        {
            return await mapper.ProjectTo<JogosDisponiveisModel>(Consultar(x => x.Usuario.IdUsuario != IdUsuario)).ToListAsync();
        }

        public async Task<IList<JogoReduzidoModel>> ObterMeusJogos()
        {
            return await mapper.ProjectTo<JogoReduzidoModel>(Consultar(x => x.Usuario.IdUsuario == IdUsuario)).ToListAsync();
        }
    }
}
