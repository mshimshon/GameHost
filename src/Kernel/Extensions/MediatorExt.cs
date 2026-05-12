using GameHost.Kernel.Extensions.Mediator;
using MedihatR;

namespace GameHost.Kernel.Extensions;

public static class MediatorExt
{
    public static HandlerBuilder Handle(this IRequest request, Func<CancellationToken, Task> handler, Func<Exception, Task>? onErrorFound = default)
    {
        var builder = new HandlerBuilder(async (ct) =>
        {
            await handler(ct);
            return default;
        });
        if (onErrorFound != default)
            builder.AddExceptionHandler(onErrorFound);
        return builder;
    }

    public static HandlerBuilder HandleWithData<TRequest>(this IRequest request, TRequest data, Func<TRequest, CancellationToken, Task> handler, Func<Exception, Task>? onErrorFound = default)
    {
        var builder = new HandlerBuilder(async (ct) =>
        {
            await handler(data, ct);
            return default;
        });
        if (onErrorFound != default)
            builder.AddExceptionHandler(onErrorFound);
        return builder;
    }

    public static HandlerBuilder Handle<TResult>(this IRequest request, Func<CancellationToken, Task<TResult>> handler, Func<Exception, Task>? onErrorFound = default)
    {
        var builder = new HandlerBuilder(async (ct) => await handler(ct));
        if (onErrorFound != default)
            builder.AddExceptionHandler(onErrorFound);
        return builder;
    }

    public static HandlerBuilder HandleWithData<TResult, TRequest>(this IRequest request, TRequest data, Func<TRequest, CancellationToken, Task<TResult>> handler, Func<Exception, Task>? onErrorFound = default)
    {
        var builder = new HandlerBuilder(async (ct) => await handler(data, ct));
        if (onErrorFound != default)
            builder.AddExceptionHandler(onErrorFound);
        return builder;
    }

    public static HandlerBuilder HandleExceptionFor<TException>(this HandlerBuilder builder, Func<TException, Task> onErrorFound)
        where TException : notnull, Exception
    {
        builder.AddExceptionHandler(onErrorFound);
        return builder;
    }
    public static async Task ExecAsync(this HandlerBuilder builder, CancellationToken ct = default)
    {
        await builder.ExecAsync(ct);
    }

    public static async Task<TResult> ExecAsync<TResult>(this HandlerBuilder builder, CancellationToken ct = default)
        where TResult : notnull
    {
        var result = await builder.ExecAsync(ct);
        return (TResult)result!;
    }

    public static async Task<TResult?> ExecOrDefaultAsync<TResult>(this HandlerBuilder builder, CancellationToken ct = default)
    {
        var result = await builder.ExecAsync(ct);
        if (result == default) return default;
        return (TResult)result;
    }
}
