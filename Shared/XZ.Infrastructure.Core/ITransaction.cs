using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace XZ.Infrastructure
{
    public interface ITransaction
    {
        IDbContextTransaction GetCurrentTransaction();

        bool HasActiveTransaction { get; }

        #region 异步
        Task<IDbContextTransaction> BeginTransactionAsync();

        Task CommitTransactionAsync(IDbContextTransaction transaction);

        Task RollbackTransactionAsync();
        #endregion

        #region 同步
        IDbContextTransaction BeginTransaction();

        void CommitTransaction(IDbContextTransaction transaction);

        void RollbackTransaction();
        #endregion
    }
}
