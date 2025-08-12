using System;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.FridayMediator.Abstractions;

namespace Infrastructure.FridayMediator.Behaviors;

public interface IPipelineBehavior<in TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    Task<TResponse> Handle(TRequest request, Func<Task<TResponse>> next, CancellationToken cancellationToken);
}