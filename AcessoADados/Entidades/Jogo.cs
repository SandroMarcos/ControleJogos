using System;
using System.Collections.Generic;
using System.Text;

namespace AcessoADados
{
    public class Jogo
    {
        public virtual int IdJogo { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual string Nome { get; set; }

        public virtual string Descricao { get; set; }        

        public virtual bool Emprestado { get; set; }

        public virtual IList<EmprestimoJogo> Emprestimos { get; set; }
    }
}
