using AutoMapper;
using Infra;
using Modelos;
using System;
using System.Threading.Tasks;

namespace RegrasNegocio
{
    public class ComandoAutenticarUsuario : IComandoAutenticarUsuario
    {
        private readonly IConsultaUsuario consultaUsuario;
        private readonly IMapper mapper;
        private readonly IComandoGerarTokenAutentication comandoGerarTokenAutentication;

        public int IdUsuario { get; set; }

        public ComandoAutenticarUsuario(IConsultaUsuario consultaUsuario, IMapper mapper, IComandoGerarTokenAutentication comandoGerarTokenAutentication)
        {
            this.consultaUsuario = consultaUsuario;
            this.mapper = mapper;
            this.comandoGerarTokenAutentication = comandoGerarTokenAutentication;
        }

        public async Task<RespostaAutenticacaoSucessoModel> Executar(EntradaAutenticacaoModel entrada)
        {
            var usuario = await consultaUsuario.Executar(entrada.Email);
            entrada.Senha = CriptografiaAES.Encrypt(entrada.Senha, CriptografiaAES.CHAVE_AES, CriptografiaAES.AESCryptographyLevel.AES_128);

            if (usuario == null || usuario.Senha != entrada.Senha)
            {
                throw new Exception("Usuário ou senha inválidos.");
            }

            return await comandoGerarTokenAutentication.Executar(mapper.Map<UsuarioReduzidoModel>(usuario));
        }
    }
}
