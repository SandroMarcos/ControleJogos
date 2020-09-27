using AcessoADados;
using AutoMapper;
using Infra;
using Modelos;
using Repositorios;
using System.Threading.Tasks;

namespace RegrasNegocio
{
    public class ComandoCriarUsuario : IComandoCriarUsuario
    {
        private readonly IRepositorioUsuario repositorioUsuario;
        private readonly IMapper mapper;
        private readonly IComandoGerarTokenAutentication comandoGerarTokenAutentication;

        public int IdUsuario { get; set; }

        public ComandoCriarUsuario(IRepositorioUsuario repositorioUsuario, IMapper mapper, IComandoGerarTokenAutentication comandoGerarTokenAutentication)
        {
            this.repositorioUsuario = repositorioUsuario;
            this.mapper = mapper;
            this.comandoGerarTokenAutentication = comandoGerarTokenAutentication;
        }

        public async Task<RespostaAutenticacaoSucessoModel> Executar(UsuarioModel entrada)
        {
            entrada.Senha = CriptografiaAES.Encrypt(entrada.Senha, CriptografiaAES.CHAVE_AES, CriptografiaAES.AESCryptographyLevel.AES_128);

            var usuario = mapper.Map<Usuario>(entrada);
            await repositorioUsuario.Inserir(usuario);

            return await comandoGerarTokenAutentication.Executar(mapper.Map<UsuarioReduzidoModel>(usuario));
        }
    }
}
