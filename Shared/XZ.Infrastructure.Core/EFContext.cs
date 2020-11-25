using DotNetCore.CAP;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace XZ.Infrastructure
{
    public class EFContext : DbContext, IUnitOfWork, ITransaction
    {
        protected IMediator _mediator;
        ICapPublisher _capBus;

        public EFContext(DbContextOptions options, IMediator mediator, ICapPublisher capBus) : base(options)
        {
            this._mediator = mediator;
            this._capBus = capBus;
        }

        private IDbContextTransaction _currentTransaction;

        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;
        public bool HasActiveTransaction => _currentTransaction != null;

        #region 事务处理 异步提交

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <returns></returns>
        public Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;
            _currentTransaction = Database.BeginTransaction(_capBus, autoCommit: false);
            return Task.FromResult(_currentTransaction);
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null)
                throw new ArgumentNullException(nameof(transaction));

            if (transaction != _currentTransaction)
                throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public async Task RollbackTransactionAsync()
        {
            try
            {
                await _currentTransaction?.RollbackAsync();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        #endregion

        #region 事务处理 同步提交
        /// <summary>
        /// 开启事务
        /// </summary>
        /// <returns></returns>
        public IDbContextTransaction BeginTransaction()
        {
            if (_currentTransaction != null) return null;
            _currentTransaction = Database.BeginTransaction(_capBus, autoCommit: false);
            return _currentTransaction;
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public void CommitTransaction(IDbContextTransaction transaction)
        {
            if (transaction == null)
                throw new ArgumentNullException(nameof(transaction));

            if (transaction != _currentTransaction)
                throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                SaveChanges();
                transaction.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
        #endregion

        #region IUnitOfWork
        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await base.SaveChangesAsync(cancellationToken);
            await _mediator?.DispatchDomainEventsAsync(this);
            return true;
        }

        public bool SaveEntities()
        {
            base.SaveChanges();
            _mediator?.DispatchDomainEvents(this);
            return true;
        }
        #endregion
    }
}
