using GameHost.Kernel.Extensions.Mediator.Exceptions;
using LunaticPanel.Core.Utils.Abstraction.Logging;

namespace GameHost.Kernel.Extensions.Mediator;

public class HandlerBuilder
{
    private readonly Func<CancellationToken, Task<object?>> _handler;
    private ICrazyReport? _crazyReport;
    internal HandlerBuilder(Func<CancellationToken, Task<object?>> handler)
    {
        _handler = handler;
    }

    internal async Task<object?> ExecAsync(CancellationToken ct = default)
    {
        try
        {
            Func<CancellationToken, Task<object?>>? _lastChain = default;
            for (int i = _errorTasks.Count - 1; i >= 0; i--)
            {
                var item = _errorTasks[i];
                var snapshot = _lastChain;
                if (snapshot == default)
                    _lastChain = (ct) => item(() => _handler(ct));
                else
                    _lastChain = (ct) => item(() => snapshot(ct));
            }
            if (_lastChain == default)
                return await _handler(ct);
            else
                return await _lastChain(ct);

        }
        catch (HandlerErrorFoundException)
        {
        }
        catch (Exception ex)
        {
            if (_crazyReport != default)
                _crazyReport.ReportErrorException(ex.Message, ex);
            else
                Console.Error.WriteLine(ex.Message);

        }
        return default;

    }

    private List<Func<Func<Task<object?>>, Task<object?>>> _errorTasks = new();
    internal void AddExceptionHandler<TException>(Func<TException, Task> onErrorFound)
        where TException : notnull, Exception
    {
        _errorTasks.Add(async (next) =>
        {
            try
            {
                return await next();
            }
            catch (TException ex)
            {
                await onErrorFound(ex);
                throw new HandlerErrorFoundException();
            }
            catch (Exception)
            {
                throw;
            }
        });
    }


}
