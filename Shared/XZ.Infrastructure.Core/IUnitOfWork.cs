using System;

using System.Threading;
using System.Threading.Tasks;

namespace XZ.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
        bool SaveEntities();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
    }
}
