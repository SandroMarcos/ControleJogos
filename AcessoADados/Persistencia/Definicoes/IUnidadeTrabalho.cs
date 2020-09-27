using System;
using System.Threading.Tasks;

namespace AcessoADados
{
    public interface IUnidadeTrabalho : IDisposable
    {        
        void BeginTransaction();
		void BeginTransaction(System.Data.IsolationLevel nivel);
		Task Commit();
        Task Rollback();
        void CloseTransaction();     
    }
}
