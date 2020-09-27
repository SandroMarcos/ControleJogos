using AcessoADados;
using Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositorios
{
    public interface IRepositorioEmprestimo : IRepositorioCrud<EmprestimoJogo>
    {
        Task<IList<EmprestimoReduzidoModel>> BuscarEmprestimo(int idJogo);

        Task<IList<EmprestimoReduzidoModel>> BuscarEmprestimoDoJogo(int idJogo);

        Task<IList<HistoricoEmprestimoModel>> BuscarHistoricoEmprestimo(int idJogo);

        //
    }
}
