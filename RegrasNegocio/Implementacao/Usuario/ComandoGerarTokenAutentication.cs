using AutoMapper;
using Infra.Configuracao;
using Microsoft.IdentityModel.Tokens;
using Modelos;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace RegrasNegocio
{
    public class ComandoGerarTokenAutentication : IComandoGerarTokenAutentication
    {
        private readonly IMapper mapper;

        public int IdUsuario { get; set; }

        public ComandoGerarTokenAutentication(IMapper mapper) => this.mapper = mapper;

        public async Task<RespostaAutenticacaoSucessoModel> Executar(UsuarioReduzidoModel entrada)
        {
            var tokenExpire = DateTime.UtcNow.AddDays(1);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(ControleJogosConfiguracao.ConfigurationInstance.GetSection("SecretKeys").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, entrada.Nome ),
                    new Claim(ClaimTypes.Email,entrada.Email ),
                    new Claim("IdUsuario", entrada.IdUsuario.ToString())
                }),
                Expires = tokenExpire,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var retorno = mapper.Map<RespostaAutenticacaoSucessoModel>(entrada);
            retorno.TokenExpire = tokenExpire;
            retorno.Token = tokenHandler.WriteToken(token);

            return await Task.FromResult(retorno);
        }
    }
}
