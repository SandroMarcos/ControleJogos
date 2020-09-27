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
    public class EmprestimoController : ControladoraPadrao
    {
        private readonly IConsultaHistoricoEmprestimo consultaHistoricoEmprestimo;
        private readonly IComandoEmprestarJogo comandoEmprestarJogo;
        private readonly IComandoDevolverJogo comandoDevolverJogo;
        private readonly IComandoExcluirEmprestimos comandoExcluirEmprestimos;

        public EmprestimoController(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IConsultaHistoricoEmprestimo consultaHistoricoEmprestimo,
            IComandoEmprestarJogo comandoEmprestarJogo,
            IComandoDevolverJogo comandoDevolverJogo,
            IComandoExcluirEmprestimos comandoExcluirEmprestimos)
            : base(mapper, httpContextAccessor)
        {
            this.consultaHistoricoEmprestimo = consultaHistoricoEmprestimo;
            this.comandoEmprestarJogo = comandoEmprestarJogo;
            this.comandoDevolverJogo = comandoDevolverJogo;
            this.comandoExcluirEmprestimos = comandoExcluirEmprestimos;
            
            this.consultaHistoricoEmprestimo.IdUsuario = IdUsuario;
            this.comandoEmprestarJogo.IdUsuario = IdUsuario;
            this.comandoDevolverJogo.IdUsuario = IdUsuario;
            this.comandoExcluirEmprestimos.IdUsuario = IdUsuario;
        }

        /// <summary>        
        /// Busca o histórico de empréstimos do jogo
        /// </summary>
        /// <param name="id">Id do Jogo</param>
        /// <returns>Listagem de Histórico</returns>
        [HttpGet("{id}")]
        public async Task<IEnumerable<HistoricoEmprestimoModel>> Get(int id) => await consultaHistoricoEmprestimo.Executar(id);

        /// <summary>
        /// Realiza o empréstimo de um jogo
        /// </summary>
        /// <param name="id">Id do Jogo</param>
        /// <returns>Verdadeiro para emprestimo realizado, caso contrário mensagem de erro</returns>
        [HttpPost("{id}")]
        [ControleTransacao]
        public async Task<IActionResult> Post(int id) => Ok(await comandoEmprestarJogo.Executar(id));

        /// <summary>        
        /// Realiza a devolução do jogo        
        /// </summary>
        /// <param name="id">Id do Jogo</param>
        /// <returns>Verdadeiro para devolução realizado, caso contrário mensagem de erro</returns>
        [HttpPut("{id}")]
        [ControleTransacao]
        public async Task<IActionResult> Put(int id) => Ok(await comandoDevolverJogo.Executar(id));

        /// <summary>        
        /// Exclui todo os históricos de emprestimos de um determinado jogo
        /// </summary>
        /// <returns>Vedadeiro para exclusão com sucesso, caso contrário mensagem de erro</returns>
        [HttpDelete("{id}")]
        [ControleTransacao]
        public async Task<IActionResult> Delete(int id) => Ok(await comandoExcluirEmprestimos.Executar(id));
    }
}
