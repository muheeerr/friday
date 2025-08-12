using System.Threading;
using System.Threading.Tasks;
using Infrastructure.FridayMediator.Abstractions;

namespace Infrastructure.FridayMediator.Samples;

public record PingRequest : IRequest<Result>
{
    public string Message { get; set; } = "Hello, World!";
}

public class PingHandler : IRequestHandler<PingRequest, Result>
{
    public async Task<Result> Handle(PingRequest request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return new Result
        {
            Message = "You said: " + request.Message
        };
    }
}

public record Result
{
    public string Message { get; set; } = "Pong!";
}