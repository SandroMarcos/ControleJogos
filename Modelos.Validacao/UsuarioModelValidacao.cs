using FluentValidation;
using Repositorios;
using System.Threading;
using System.Threading.Tasks;

namespace Modelos.Validacao
{
    public class UsuarioModelValidacao : AbstractValidator<UsuarioModel>
    {
        private readonly IRepositorioUsuario repositorioUsuario;

        public UsuarioModelValidacao(IRepositorioUsuario repositorioUsuario)
        {
            this.repositorioUsuario = repositorioUsuario;

            RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O campo nome deve ser informado")
            .Length(4, 150).WithMessage("O campo nome deve ter entre 4 e 150 caracteres");

            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O campo e-mail deve ser informado")
            .EmailAddress().WithMessage("E-mail é inválido")
            .Length(4, 100).WithMessage("O campo nome deve ter entre 4 e 100 caracteres");

            RuleFor(x => x.Senha)
            .NotEmpty().WithMessage("O campo senha deve ser informado")
            .Length(3, 150).WithMessage("O campo nome deve ter entre 3 e 150 caracteres");

            RuleFor(x => x)
            .MustAsync(ValidarUsuario)
            .WithMessage("E-mail já cadastrado.");
        }

        private async Task<bool> ValidarUsuario(UsuarioModel usuario, CancellationToken arg)
        {
            if (usuario.IdUsuario > 0) return true;
            
            return await repositorioUsuario.Existe(x => x.Email.ToLower().Trim() == usuario.Email.ToLower()) == false;
        }
    }
}
