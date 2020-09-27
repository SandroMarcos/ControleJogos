using Modelos;
using System.Collections.Generic;

namespace RegrasNegocio
{
    public interface IConsultaHistoricoEmprestimo : IComando<int,IList<HistoricoEmprestimoModel>>
    {
    }
}
