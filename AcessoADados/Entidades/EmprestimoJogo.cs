using System;
using System.Collections.Generic;
using System.Text;

namespace AcessoADados
{
    public class EmprestimoJogo
    {
        public virtual int IdEmprestimoJogo { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual Jogo Jogo  { get; set; }

        public virtual DateTime DataEmprestimo { get; set; }

        public virtual DateTime? DataDevolucao { get; set; }
    }
}
