using AutoMapper;
using ControleJogosApi.Filtros;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelos;
using RegrasNegocio;
using System.Threading.Tasks;

namespace ControleJogosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControladoraPadrao
    {
        private readonly IComandoAlterarUsuario comandoAlterarUsuario;
        private readonly IComandoExcluirUsuario comandoExcluirUsuario;

        public UsuarioController(
            IMapper mapper, 
            IHttpContextAccessor httpContextAccessor,
            IComandoAlterarUsuario comandoAlterarUsuario,
            IComandoExcluirUsuario comandoExcluirUsuario) 
            : base(mapper, httpContextAccessor)
        {
            this.comandoAlterarUsuario = comandoAlterarUsuario;
            this.comandoExcluirUsuario = comandoExcluirUsuario;

            this.comandoAlterarUsuario.IdUsuario = IdUsuario;
            this.comandoExcluirUsuario.IdUsuario = IdUsuario;
        }

        /// <summary>
        /// Altera dados do usuário
        /// </summary>
        /// <param name="modelo">Modelo de usuário</param>
        /// <returns>Modelo de usuários</returns>
        [HttpPut]
        [ControleTransacao]
        public async Task<UsuarioReduzidoModel> Put([FromBody] UsuarioModel modelo) => await comandoAlterarUsuario.Executar(modelo);

        /// <summary>
        /// Exclui um usuário
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <returns>Verdadeiro para exclusãp realizado com sucesso, caso contrário mensagem de erro</returns>
        [HttpDelete("{id}")]
        [ControleTransacao]
        public async Task<IActionResult> Delete(int id) => Ok(await comandoExcluirUsuario.Executar(id));
    }
}
