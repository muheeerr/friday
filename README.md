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

## Pipeline Behaviors
Implement `IPipelineBehavior<TRequest, TResponse>` for cross-cutting logic (logging, validation, etc).

## License
MIT

## Links
- [NuGet](https://www.nuget.org/packages/FridayMediator)
- [GitHub](https://github.com/muheeerr/Friday)
