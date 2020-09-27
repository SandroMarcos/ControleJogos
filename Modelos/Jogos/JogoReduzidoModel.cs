using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos
{
    public class JogoReduzidoModel
    {
        public  int IdJogo { get; set; }       

        public  string Nome { get; set; }

        public  string Descricao { get; set; }        

        public bool Emprestado { get; set; }        
    }
}
