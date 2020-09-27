using AcessoADados;
using AutoMapper;
using Infra;
using Modelos;
using Repositorios;
using System;
using System.Threading.Tasks;

namespace RegrasNegocio
{
    public class ComandoAlterarUsuario : IComandoAlterarUsuario
    {        
        private readonly IRepositorioUsuario repositorioUsuario;
        private readonly IMapper mapper;

        public int IdUsuario { get; set; }

        public ComandoAlterarUsuario(IRepositorioUsuario repositorioUsuario, IMapper mapper)
        {            
            this.repositorioUsuario = repositorioUsuario;
            this.mapper = mapper;
        }

        public async Task<UsuarioReduzidoModel> Executar(UsuarioModel entrada)
        {
            if (entrada.IdUsuario != IdUsuario)
            {
                throw new Exception("Usuário não encontrado.");
            }

            var usuario = await repositorioUsuario.ObterPorId(entrada.IdUsuario);

            if (usuario == null)
            {
                throw new Exception("Usuário não cadastrado.");
            }
            
            if (string.IsNullOrEmpty(entrada.Senha) == false)
            {
                usuario.Senha = CriptografiaAES.Encrypt(entrada.Senha, CriptografiaAES.CHAVE_AES, CriptografiaAES.AESCryptographyLevel.AES_128);
            }

            usuario.Email = entrada.Email;
            usuario.Nome = entrada.Nome;

            await repositorioUsuario.Alterar(usuario);

            return mapper.Map<UsuarioReduzidoModel>(usuario);
        }
    }
}
