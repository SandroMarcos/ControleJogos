using Modelos;
using Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RegrasNegocio
{
    public class ConsultaHistoricoEmprestimo : IConsultaHistoricoEmprestimo
    {
        private readonly IRepositorioEmprestimo repositorioEmprestimo;        

        public int IdUsuario { set => repositorioEmprestimo.IdUsuario = value; }

        public ConsultaHistoricoEmprestimo(IRepositorioEmprestimo repositorioEmprestimo) => this.repositorioEmprestimo = repositorioEmprestimo;

        public async Task<IList<HistoricoEmprestimoModel>> Executar(int entrada) => await repositorioEmprestimo.BuscarHistoricoEmprestimo(entrada);
    }
}
