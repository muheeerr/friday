# FridayMediator

A lightweight, extensible Mediator implementation for .NET, inspired by Tony Stark's Friday AI assistant. Supports request/response, notifications, and pipeline behaviors.

## Features
- Decoupled request/response and notification handling
- Pipeline behaviors for cross-cutting concerns
- Dependency injection support
- Simple, framework-agnostic design

## Installation
```
dotnet add package FridayMediator
```

## Usage

### 1. Define a Request and Handler
```csharp
public record PingRequest : IRequest<string>;

public class PingHandler : IRequestHandler<PingRequest, string>
{
    public Task<string> Handle(PingRequest request, CancellationToken cancellationToken)
        => Task.FromResult("Pong!");
}
```

### 2. Register FridayMediator in DI
```csharp
services.AddFriday(typeof(PingHandler).Assembly);
```

### 3. Send a Request
```csharp
var mediator = serviceProvider.GetRequiredService<IFriday>();
var response = await mediator.Send(new PingRequest());
```

# Example: Using Notifications and Queries

Below is an example of how to use a notification and a query with Friday Mediator:

```csharp
// Notification definition and handler
public class Notification
{
    public record GetDateNotification(string Message) : INotification;

    public class GetDateNotificationHandler : INotificationHandler<GetDateNotification>
    {
        public async Task Handle(GetDateNotification notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Date notification received: {notification.Message} at {DateTime.Now}");
            await Task.Delay(2000, cancellationToken);
            Console.WriteLine($"Date notification processed at {DateTime.Now}");
        }
    }
}

// Query definition and handler
public record GetDateQuery : IRequest<DateTime>;

public class GetDateQueryHandler(IFriday mediator) : IRequestHandler<GetDateQuery, DateTime>
{
    private readonly IFriday _mediator = mediator;

    public async Task<DateTime> Handle(GetDateQuery request, CancellationToken cancellationToken)
    {
        await _mediator.Publish(new Notification.GetDateNotification("GetDateQuery executed"), cancellationToken);
        Console.WriteLine($"GetDateQueryHandler executed at: {DateTime.Now}");
        return DateTime.Now;
    }
}

// Minimal API endpoint example
app.MapGet("/api/date", async (IFriday mediator, CancellationToken cancellationToken) =>
{
    var date = await mediator.Send(new GetDateQuery(), cancellationToken);
    return Results.Ok(date);
});
```

This demonstrates how to define and handle notifications and queries, and how to expose them via a minimal API endpoint.


## Pipeline Behaviors
Implement `IPipelineBehavior<TRequest, TResponse>` for cross-cutting logic (logging, validation, etc).

## License
MIT

## Links
- [NuGet](https://www.nuget.org/packages/FridayMediator)
- [GitHub](https://github.com/muheeerr/Friday)
