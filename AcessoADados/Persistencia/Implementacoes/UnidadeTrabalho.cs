using NHibernate;
using System;
using System.Threading.Tasks;

namespace AcessoADados
{
    public class UnidadeTrabalho : IUnidadeTrabalho
    {
        private readonly ISession sessao;
        private ITransaction transacao;

        public UnidadeTrabalho(ISession _sessao) => sessao = _sessao;

        public ISession Sessao => sessao;

        /// <summary>
        /// Inicia uma transação no BD.
        /// </summary>
        public void BeginTransaction() => transacao = sessao.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);

        /// <summary>
        /// Inicia uma transação no BD atribuindo um nivel de isolamento personalizado.
        /// </summary>
        public void BeginTransaction(System.Data.IsolationLevel nivel) => transacao = sessao.BeginTransaction(nivel);

        /// <summary>
        /// <see cref="Comita"/> a transação corrente.
        /// </summary>
        public async Task Commit()
        {
            await sessao.FlushAsync();

            if (transacao.WasCommitted == false)
            {
                await transacao.CommitAsync();
            }

            sessao.Clear();
        }

        /// <summary>
        /// Cancela a transação corrente.
        /// </summary>
        public async Task Rollback()
        {
            await transacao.RollbackAsync();
            sessao.Clear();
        }

        public void CloseTransaction()
        {
            if (transacao != null)
            {
                transacao.Dispose();
                transacao = null;
            }
        }

        /// <summary>
        /// Método para atender a interface <see cref="IDisposable"/>.
        /// </summary>
        public void Dispose()
        {
            sessao.Close();
            sessao.Dispose();
        }
    }
}
