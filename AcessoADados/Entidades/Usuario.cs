using System.Collections.Generic;

namespace AcessoADados
{
    public class Usuario
    {
        public virtual int IdUsuario { get; set; }        

        public virtual string Nome { get; set; }

        public virtual string Email { get; set; }

        public virtual string Senha { get; set; }

        public virtual IList<Jogo> Jogos  { get; set; }

        public virtual IList<EmprestimoJogo> Emprestimos { get; set; }
    }
}
