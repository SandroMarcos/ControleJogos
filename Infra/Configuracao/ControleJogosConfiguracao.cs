using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Configuracao
{
    public class ControleJogosConfiguracao
    {
        public static IConfiguration ConfigurationInstance { get; set; }
    }
}
