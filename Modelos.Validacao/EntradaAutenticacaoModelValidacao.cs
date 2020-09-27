using FluentValidation;
using Repositorios;
using System.Threading;
using System.Threading.Tasks;

namespace Modelos.Validacao
{
    public class EntradaAutenticacaoModelValidacao : AbstractValidator<EntradaAutenticacaoModel>
    {
        private readonly IRepositorioUsuario repositorioUsuario;

        public EntradaAutenticacaoModelValidacao(IRepositorioUsuario repositorioUsuario)
        {
            this.repositorioUsuario = repositorioUsuario;

            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O campo e-mail deve ser informado")
            .EmailAddress().WithMessage("E-mail é inválido")
            .Length(4, 100).WithMessage("O campo nome deve ter entre 4 e 100 caracteres");

            RuleFor(x => x.Senha)
            .NotEmpty().WithMessage("O campo senha deve ser informado")
            .Length(3, 150).WithMessage("O campo nome deve ter entre 3 e 150 caracteres");

            RuleFor(x => x.Email)
            .MustAsync(ValidarUsuario)
            .WithMessage("Usuário ou senha inválidos.");
        }

        private async Task<bool> ValidarUsuario(string email, CancellationToken arg)
        {            
            return await repositorioUsuario.Existe(x => x.Email.ToLower().Trim() == email.ToLower());
        }
    }
}
