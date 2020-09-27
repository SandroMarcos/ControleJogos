using System;

namespace Modelos
{
    public class EmprestimoReduzidoModel
    {
        public int IdEmprestimoJogo { get; set; }

        public int IdUsuario { get; set; }

        public int IdJogo { get; set; }

        public DateTime DataEmprestimo { get; set; }

        public DateTime? DataDevolucao { get; set; }
    }
}
