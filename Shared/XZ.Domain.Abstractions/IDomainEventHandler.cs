using MediatR;

namespace XZ.Domain
{
    //由领域事件接口调用处理
    public interface IDomainEventHandler<TDomainEvent> : INotificationHandler<TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
    }
}
