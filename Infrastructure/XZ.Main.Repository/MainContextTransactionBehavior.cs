using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
using XZ.Infrastructure;

namespace XZ.Main.Repository
{
    public class MainContextTransactionBehavior<TRequest, TResponse> : TransactionBehavior<MainContext, TRequest, TResponse>
    {
        public MainContextTransactionBehavior(MainContext dbContext, ICapPublisher capBus, ILogger<MainContextTransactionBehavior<TRequest, TResponse>> logger) : base(dbContext, capBus, logger)
        {
        }
    }
}
