using AutoMapper;
using ControleJogosApi.Filtros;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelos;
using RegrasNegocio;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleJogosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JogoController : ControladoraPadrao
    {
        private readonly IConsultaMeusJogos consultaMeusJogos;
        private readonly IConsultaJogo consultaJogo;
        private readonly IConsultaJogosDisponiveis consultaJogosDisponiveis;
        private readonly IComandoSalvarJogo comandoSalvarJogo;
        private readonly IComandoExcluirJogo comandoExcluirJogo;

        public JogoController(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IConsultaMeusJogos consultaMeusJogos,
            IConsultaJogo consultaJogo,
            IConsultaJogosDisponiveis consultaJogosDisponiveis,
            IComandoSalvarJogo comandoSalvarJogo,
            IComandoExcluirJogo comandoExcluirJogo)
            : base(mapper, httpContextAccessor)
        {
            this.consultaMeusJogos = consultaMeusJogos;
            this.consultaJogo = consultaJogo;
            this.consultaJogosDisponiveis = consultaJogosDisponiveis;
            this.comandoSalvarJogo = comandoSalvarJogo;
            this.comandoExcluirJogo = comandoExcluirJogo;

            this.consultaMeusJogos.IdUsuario = IdUsuario;
            this.consultaJogo.IdUsuario = IdUsuario;
            this.consultaJogosDisponiveis.IdUsuario = IdUsuario;
            this.comandoSalvarJogo.IdUsuario = IdUsuario;
            this.comandoExcluirJogo.IdUsuario = IdUsuario;
        }

        /// <summary>        
        /// Busca todos os jogos do usuário logado
        /// </summary>
        /// <returns>coleção de jogos</returns>
        [HttpGet]
        public async Task<IEnumerable<JogoReduzidoModel>> Get() => await consultaMeusJogos.Executar();

        /// <summary>        
        /// Busca os dados de um determinado jogo
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Dados do jogo</returns>
        [HttpGet("{id}")]
        public async Task<JogoModel> Get(int id) => await consultaJogo.ObterPorId(id) ?? new JogoModel();

        /// <summary>
        /// Busca os jogos disponíveis dos outros usuários
        /// </summary>
        /// <returns>Jogos disponíveis</returns>
        [HttpGet("JogosDisponiveis")]
        public async Task<IEnumerable<JogosDisponiveisModel>> ObterJogosDisponiveis() => await consultaJogosDisponiveis.Executar();

        /// <summary>
        /// Insere um novo jogo
        /// </summary>
        /// <param name="modelo">Modelo de entrada</param>
        /// <returns>Modelo de jogos</returns>
        [HttpPost]
        [ControleTransacao]
        public async Task<JogoReduzidoModel> Post([FromBody] JogoReduzidoModel modelo) => await comandoSalvarJogo.Executar(mapper.Map<JogoModel>(modelo));

        /// <summary>
        /// Altera um jogo
        /// </summary>
        /// <param name="modelo">Modelo de entrada</param>
        /// <returns>Modelo de jogos</returns>
        [HttpPut]
        [ControleTransacao]
        public async Task<JogoReduzidoModel> Put([FromBody] JogoReduzidoModel modelo) => await comandoSalvarJogo.Executar(mapper.Map<JogoModel>(modelo));

        /// <summary>
        /// Exclui um jogo
        /// </summary>
        /// <param name="id">Id do Jogo</param>
        /// <returns>Verdadeiro para exclusão realizada, caso contrário mensagem de erro</returns>
        [HttpDelete("{id}")]
        [ControleTransacao]
        public async Task<IActionResult> Delete(int id) => Ok(await comandoExcluirJogo.Executar(id));

        /// <summary>
        /// Exclui uma lista de jogos, porém o jogo não poderá ter um histórico de emprestimos
        /// </summary>
        /// <param name="codigos">coleção de id's de jogos</param>
        /// <returns>Verdadeiro para exclusões realizadas, caso contrário mensagem de erro</returns>
        [HttpDelete("ExcluirJogos")]
        [ControleTransacao]
        public async Task<IActionResult> ExcluirJogos(int[] codigos) => Ok(await comandoExcluirJogo.ExcluirJogos(codigos));
    }
}
