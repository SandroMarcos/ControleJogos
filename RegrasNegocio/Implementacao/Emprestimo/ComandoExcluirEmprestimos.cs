using AcessoADados;
using Repositorios;
using System.Threading.Tasks;

namespace RegrasNegocio
{
    public class ComandoExcluirEmprestimos : IComandoExcluirEmprestimos
    {
        private readonly IRepositorioEmprestimo repositorioEmprestimo;
        private readonly IComandoAlterarEmprestimoJogo comandoAlterarEmprestimoJogo;

        public int IdUsuario { set => repositorioEmprestimo.IdUsuario = value; }

        public ComandoExcluirEmprestimos(IRepositorioEmprestimo repositorioEmprestimo, IComandoAlterarEmprestimoJogo comandoAlterarEmprestimoJogo)
        {
            this.repositorioEmprestimo = repositorioEmprestimo;
            this.comandoAlterarEmprestimoJogo = comandoAlterarEmprestimoJogo;
        }

        public async Task<bool> Executar(int idJogo)
        {
            var listagemEmprestimos = await repositorioEmprestimo.BuscarEmprestimoDoJogo(idJogo);
            EmprestimoJogo emprestimoJogo;

            foreach (var emprestimo in listagemEmprestimos)
            {
                if (emprestimo.DataDevolucao.HasValue == false)
                {
                    await comandoAlterarEmprestimoJogo.Executar(new Modelos.JogoEmprestimoModel { Emprestado = false, IdJogo = emprestimo.IdJogo });
                }

                emprestimoJogo = await repositorioEmprestimo.ObterPorId(emprestimo.IdEmprestimoJogo);
                await repositorioEmprestimo.Excluir(emprestimoJogo);
            }

            return true;
        }
    }
}
