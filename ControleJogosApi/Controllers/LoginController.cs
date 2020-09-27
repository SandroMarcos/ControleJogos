using AutoMapper;
using ControleJogosApi.Filtros;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos;
using RegrasNegocio;
using System;
using System.Threading.Tasks;

namespace ControleJogosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly IComandoAutenticarUsuario comandoAutenticarUsuario; 
        private readonly IComandoCriarUsuario comandoCriarUsuario;        

        public LoginController(IComandoAutenticarUsuario comandoAutenticarUsuario,
                               IComandoCriarUsuario comandoCriarUsuario,
                               IMapper mapper)
        {
            this.comandoAutenticarUsuario = comandoAutenticarUsuario;
            this.comandoCriarUsuario = comandoCriarUsuario;
        }

        /// <summary>
        /// Cria um novo usuário
        /// </summary>
        /// <param name="entrada">Modelo de usuário</param>
        /// <returns>Dados para autenticação</returns>
        [HttpPost("create")]
        [AllowAnonymous]
        [ControleTransacao]
        public async Task<IActionResult> Criar(UsuarioModel entrada)
        {
            var usuario = await comandoCriarUsuario.Executar(entrada);

            if (usuario == null)
            {
                throw new Exception("Não foi possível criar o usuário");
            }

            return Ok(usuario);
        }

        /// <summary>
        /// Autentica usuário
        /// </summary>
        /// <param name="entrada">Modelo para autenticação</param>
        /// <returns>Dados para autenticação</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Autenticar(EntradaAutenticacaoModel entrada)
        {
            var usuarioModel = await comandoAutenticarUsuario.Executar(entrada);

            if (usuarioModel == null)
            {
                return BadRequest(new RespostaErroModel("Usuário ou senha inválidos."));
            }

            return Ok(usuarioModel);
        }
    }
}
