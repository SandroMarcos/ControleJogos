using AcessoADados;
using AutoMapper;
using Modelos;
using NHibernate.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorios
{
    public class RepositorioEmprestimo : RepositorioBase<EmprestimoJogo>, IRepositorioEmprestimo
    {
        public int IdUsuario { get; set; }

        public RepositorioEmprestimo(IRepositorio _repositorio, IMapper _mapper) : base(_repositorio, _mapper) { }

        public async Task<IList<EmprestimoReduzidoModel>> BuscarEmprestimo(int idJogo)
        {
            return await mapper.ProjectTo<EmprestimoReduzidoModel>(Consultar(x => x.Jogo.IdJogo == idJogo && x.DataDevolucao.HasValue == false)).ToListAsync();
        }

        public async Task<IList<EmprestimoReduzidoModel>> BuscarEmprestimoDoJogo(int idJogo)
        {
            return await mapper.ProjectTo<EmprestimoReduzidoModel>(Consultar(x => x.Jogo.IdJogo == idJogo && x.Jogo.Usuario.IdUsuario == IdUsuario )).ToListAsync();
        }

        public async Task<IList<HistoricoEmprestimoModel>> BuscarHistoricoEmprestimo(int idJogo)
        {
            return await mapper.ProjectTo<HistoricoEmprestimoModel>(Consultar(x => x.Jogo.IdJogo == idJogo).OrderBy(x => x.DataEmprestimo)).ToListAsync();
        }
    }
}
