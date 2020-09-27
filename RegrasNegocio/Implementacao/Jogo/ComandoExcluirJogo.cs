using Repositorios;
using System;
using System.Threading.Tasks;

namespace RegrasNegocio
{
    public class ComandoExcluirJogo : IComandoExcluirJogo
    {
        private readonly IRepositorioJogo repositorioJogo;

        public int IdUsuario { get; set; }

        public ComandoExcluirJogo(IRepositorioJogo repositorioJogo)
        {
            this.repositorioJogo = repositorioJogo;
        }

        public async Task<bool> Executar(int entrada)
        {
            try
            {
                var jogo = await repositorioJogo.ObterPorId(entrada);

                if (jogo == null)
                {
                    throw new Exception("Jogo não encontrado");
                }

                if (jogo.Usuario.IdUsuario != IdUsuario)
                {
                    throw new Exception("Jogo não pode ser excluido.");
                }

                if (jogo.Emprestado)
                {
                    throw new Exception("Jogo emprestado, não pode ser excluido");
                }

                if (jogo.Emprestimos.Count > 0)
                {
                    throw new Exception("Jogo possui histórico de empréstimos, não pode ser excluido");
                }
                
                await repositorioJogo.Excluir(jogo);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ExcluirJogos(int[] codigos)
        {
            foreach (var item in codigos)
            {
                await Executar(item);
            }

            return true;
        }
    }
}
