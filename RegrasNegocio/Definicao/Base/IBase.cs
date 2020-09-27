using System.Threading.Tasks;

namespace RegrasNegocio
{
    public interface IBase<T> 
    {
        int IdUsuario { set; }
        Task<T> ObterPorId(int id);        
    }
}
