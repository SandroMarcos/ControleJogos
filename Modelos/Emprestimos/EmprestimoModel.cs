namespace Modelos
{
    public class EmprestimoModel : EmprestimoReduzidoModel
    {
        public UsuarioModel Usuario { get; set; }

        public JogoModel Jogo { get; set; }
    }
}
