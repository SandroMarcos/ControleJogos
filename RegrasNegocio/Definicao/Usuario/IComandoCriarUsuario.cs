using Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegrasNegocio
{
    public interface IComandoCriarUsuario : IComando<UsuarioModel, RespostaAutenticacaoSucessoModel>
    {
    }
}
