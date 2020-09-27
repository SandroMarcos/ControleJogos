using AcessoADados;
using AutoMapper;
using Modelos;
using Repositorios;
using System;
using System.Threading.Tasks;

namespace RegrasNegocio
{
    public class ComandoEmprestarJogo : IComandoEmprestarJogo
    {
        private readonly IComandoAlterarEmprestimoJogo comandoAlterarEmprestimoJogo;
        private readonly IConsultaJogo consultaJogo;
        private readonly IRepositorioEmprestimo repositorioEmprestimo;
        private readonly IMapper mapper;

        public ComandoEmprestarJogo(IComandoAlterarEmprestimoJogo comandoAlterarEmprestimoJogo,            
            IConsultaJogo consultaJogo,
            IRepositorioEmprestimo repositorioEmprestimo, 
            IMapper mapper)
        {
            this.comandoAlterarEmprestimoJogo = comandoAlterarEmprestimoJogo;
            this.consultaJogo = consultaJogo;
            this.repositorioEmprestimo = repositorioEmprestimo;
            this.mapper = mapper;
        }

        public int IdUsuario { get; set; }

        public async Task<bool> Executar(int idJogo)
        {
            try
            {                
                var jogo = await consultaJogo.ObterPorId(idJogo);

                if (jogo.IdUsuario == IdUsuario)
                {
                    throw new Exception("Jogo é seu, operação cancelada.");
                }
                else if (jogo.Emprestado)
                {
                    throw new Exception("Jogo emprestado, operação cancelada.");
                }

                await comandoAlterarEmprestimoJogo.Executar(new JogoEmprestimoModel { Emprestado = true, IdJogo = idJogo });

                var emprestimo = new EmprestimoReduzidoModel
                {
                    DataEmprestimo = DateTime.Now,
                    IdJogo = idJogo,
                    IdUsuario = IdUsuario
                };

                await repositorioEmprestimo.Inserir(mapper.Map<EmprestimoJogo>(emprestimo));

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
