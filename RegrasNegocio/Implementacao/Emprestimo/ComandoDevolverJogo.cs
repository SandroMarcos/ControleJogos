using Modelos;
using Repositorios;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RegrasNegocio
{
    public class ComandoDevolverJogo : IComandoDevolverJogo
    {
        private readonly IRepositorioEmprestimo repositorioEmprestimo;
        private readonly IComandoAlterarEmprestimoJogo comandoAlterarEmprestimoJogo;

        public int IdUsuario { get; set; }

        public ComandoDevolverJogo(IRepositorioEmprestimo repositorioEmprestimo, IComandoAlterarEmprestimoJogo comandoAlterarEmprestimoJogo)
        {
            this.repositorioEmprestimo = repositorioEmprestimo;
            this.comandoAlterarEmprestimoJogo = comandoAlterarEmprestimoJogo;
        }

        public async Task<bool> Executar(int idJogo)
        {
            var emprestimos = await repositorioEmprestimo.BuscarEmprestimo(idJogo);

            if (emprestimos.Count == 0)
            {
                throw new Exception("Jogo não está emprestado.");
            }
            else if (emprestimos.Count > 1)
            {
                throw new Exception("Erro na base de dados, não será possivel realizar a devolução.");
            }

            var emprestimo = emprestimos.First();

            if (emprestimo.IdUsuario != IdUsuario)
            {
                throw new Exception("Jogo não emprestado para voce, operação cancelada.");
            }

            var alterado = await repositorioEmprestimo.ObterPorId(emprestimo.IdEmprestimoJogo);
            alterado.DataDevolucao = DateTime.Now;
            await repositorioEmprestimo.Alterar(alterado);

            await comandoAlterarEmprestimoJogo.Executar(new JogoEmprestimoModel { Emprestado = false, IdJogo = emprestimo.IdJogo });

            return true;
        }
    }
}
