using AcessoADados;
using AutoMapper;
using NHibernate.Linq;
using System.Threading.Tasks;

namespace Repositorios
{
    public class RepositorioUsuario : RepositorioBase<Usuario>, IRepositorioUsuario
    {
        public RepositorioUsuario(IRepositorio _repositorio, IMapper _mapper) : base(_repositorio, _mapper) { }

        public int IdUsuario { get; set; }        

        public async Task<Usuario> ObterUsuario(string email) => await Consultar(c => c.Email == email).FirstOrDefaultAsync();
    }
}
