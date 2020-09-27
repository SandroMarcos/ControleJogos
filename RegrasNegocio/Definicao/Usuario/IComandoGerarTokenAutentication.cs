using Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegrasNegocio
{
    public interface IComandoGerarTokenAutentication : IComando<UsuarioReduzidoModel, RespostaAutenticacaoSucessoModel>
    {
    }
}
