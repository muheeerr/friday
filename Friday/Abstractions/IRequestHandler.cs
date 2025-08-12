
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.FridayMediator.Abstractions;

public interface IRequestHandler<in TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}

