using AcessoADados;
using AutoMapper;
using Modelos;
using Repositorios;
using System;
using System.Threading.Tasks;

namespace RegrasNegocio
{
    public class ComandoSalvarJogo : IComandoSalvarJogo
    {
        private readonly IRepositorioJogo repositorioJogo;
        private readonly IMapper mapper;
        private int idUsuario;

        public int IdUsuario 
        { 
            set 
            { 
                repositorioJogo.IdUsuario = value;
                idUsuario = value;
            } 
        }        

        public ComandoSalvarJogo(IRepositorioJogo repositorioJogo, IMapper mapper)
        {
            this.repositorioJogo = repositorioJogo;
            this.mapper = mapper;
        }

        public async Task<JogoReduzidoModel> Executar(JogoModel entrada)
        {
            entrada.IdUsuario = idUsuario;
            var jogo = mapper.Map<Jogo>(entrada);            

            if (jogo.IdJogo == 0)
            {
                jogo.IdJogo = (int)await repositorioJogo.Inserir(jogo);
            }            
            else if (await repositorioJogo.Existe(x => x.IdJogo == jogo.IdJogo && x.Usuario.IdUsuario == idUsuario) == false)
            {
                throw new Exception("Jogo não existe ou não lhe pertence, operação cancelada");
            }
            else
            {
                jogo = await repositorioJogo.ObterPorId(jogo.IdJogo);

                jogo.Descricao = entrada.Descricao;
                jogo.Nome = entrada.Nome;                    
                jogo.Emprestado = entrada.Emprestado;

                await repositorioJogo.Alterar(jogo);
            }            

            return mapper.Map<JogoReduzidoModel>(jogo);
        }
    }
}
