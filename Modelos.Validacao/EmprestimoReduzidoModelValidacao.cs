using FluentValidation;

namespace Modelos.Validacao
{
    public class EmprestimoReduzidoModelValidacao : AbstractValidator<EmprestimoReduzidoModel>
    {
        public EmprestimoReduzidoModelValidacao()
        {
            RuleFor(x => x.IdJogo)
            .NotEmpty()
            .WithMessage("Jogo deve ser informado.");
        }
    }
}
