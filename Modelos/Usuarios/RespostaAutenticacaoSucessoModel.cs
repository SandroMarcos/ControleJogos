using System;

namespace Modelos
{
    public class RespostaAutenticacaoSucessoModel : UsuarioReduzidoModel
    {
        public string Token { get; set; }

        public DateTime TokenExpire { get; set; }
    }
}
