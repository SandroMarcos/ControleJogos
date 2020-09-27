using Repositorios;
using System;
using System.Threading.Tasks;

namespace RegrasNegocio
{
    public class ComandoExcluirUsuario : IComandoExcluirUsuario
    {
        private readonly IRepositorioUsuario repositorioUsuario;

        public int IdUsuario { get; set; }

        public ComandoExcluirUsuario(IRepositorioUsuario repositorioUsuario)
        {
            this.repositorioUsuario = repositorioUsuario;
        }

        public async Task<bool> Executar(int entrada)
        {
            try
            {
                var usuario = await repositorioUsuario.ObterPorId(entrada);

                if (usuario == null || usuario.IdUsuario != IdUsuario)
                {
                    throw new Exception("Usuário não encontrado");
                }

                if (usuario.Jogos.Count > 0)
                {
                    throw new Exception("Usuário possui jogos cadastrados, não pode ser excluido");
                }                
                
                await repositorioUsuario.Excluir(usuario);

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
