using Modelos;
using Repositorios;
using System;
using System.Threading.Tasks;

namespace RegrasNegocio
{
    public class ComandoAlterarEmprestimoJogo : IComandoAlterarEmprestimoJogo
    {
        private readonly IRepositorioJogo repositorioJogo;

        public ComandoAlterarEmprestimoJogo(IRepositorioJogo repositorioJogo)
        {
            this.repositorioJogo = repositorioJogo;
        }

        public int IdUsuario { set => throw new NotImplementedException(); }

        public async Task<bool> Executar(JogoEmprestimoModel entrada)
        {
            var jogo = await repositorioJogo.ObterPorId(entrada.IdJogo);
            jogo.Emprestado = entrada.Emprestado;
            await repositorioJogo.Alterar(jogo);

            return true;
        }
    }
}
