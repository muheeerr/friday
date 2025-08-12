using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.FridayMediator.Abstractions;

public interface INotificationHandler<in TNotification>
    where TNotification : INotification
{
    Task Handle(TNotification notification, CancellationToken cancellationToken);
}