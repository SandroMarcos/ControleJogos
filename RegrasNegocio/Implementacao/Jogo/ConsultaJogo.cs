using AutoMapper;
using Modelos;
using Repositorios;
using System;
using System.Threading.Tasks;

namespace RegrasNegocio
{
    public class ConsultaJogo : IConsultaJogo
    {
        private readonly IRepositorioJogo repositorioJogo;
        private readonly IMapper mapper;

        public int IdUsuario { get; set; }

        public ConsultaJogo(IRepositorioJogo repositorioJogo, IMapper mapper)
        {
            this.repositorioJogo = repositorioJogo;
            this.mapper = mapper;
        }

        public async Task<JogoModel> ObterPorId(int id)
        {
            var jogo = await repositorioJogo.ObterPorId(id);            

            if (jogo == null)
            {
                throw new Exception("Jogo não encontrado");
            }

            return mapper.Map<JogoModel>(jogo);
        }
    }
}
