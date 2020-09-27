using FluentValidation;

namespace Modelos.Validacao
{
    public class JogoReduzidoModelValidacao : AbstractValidator<JogoReduzidoModel>
    {
        public JogoReduzidoModelValidacao()
        {
            RuleFor(x => x.Descricao)
            .NotEmpty().WithMessage("O campo descricao deve ser informado")
            .Length(1, 250).WithMessage("O campo nome deve ter entre 1 e 250 caracteres");

            RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O campo nome deve ser informado")
            .Length(3, 150).WithMessage("O campo nome deve ter entre 3 e 250 caracteres");
        }
    }
}
