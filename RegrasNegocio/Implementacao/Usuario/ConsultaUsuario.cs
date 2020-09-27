using AcessoADados;
using AutoMapper;
using Modelos;
using Repositorios;
using System;
using System.Threading.Tasks;

namespace RegrasNegocio
{
    public class ConsultaUsuario : IConsultaUsuario
    {
        private readonly IRepositorioUsuario usuario;
        private readonly IMapper mapper;

        public ConsultaUsuario(IRepositorioUsuario _usuario, IMapper _mapper)
        {
            usuario = _usuario;
            mapper = _mapper;
        }

        public int IdUsuario { get; set; }        

        public async Task<Usuario> Executar(int id) => await usuario.ObterPorId(id);

        public async Task<Usuario> Executar(string email) => mapper.Map<Usuario>(await usuario.ObterUsuario(email));
    }
}
