using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Behaviours;

public class LogginBehaviour<TRequest, TResponse>(
    ILogger<LogginBehaviour<TRequest, TResponse>> logger
) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        logger.LogInformation(
            "MediatR Request: {RequestName} {@Request}",
            typeof(TRequest).Name,
            request
        );

        var timer = new Stopwatch();
        timer.Start();

        var resposne = await next();

        timer.Stop();

        var timeTaken = timer.Elapsed;

        if (timeTaken.Seconds > 3)
        {
            logger.LogWarning(
                "MediatR Request: {RequestName} {@Request} took {TimeTaken} seconds",
                typeof(TRequest).Name,
                request,
                timeTaken.Seconds
            );
        }

        logger.LogInformation(
            "End MediatR Request: {RequestName} with {Response}",
            typeof(TRequest).Name,
            typeof(TResponse)
        );

        return resposne;
    }
}
