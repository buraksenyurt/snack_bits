using Serilog;

namespace Application.Trace;

public class SerilogAdapter : ILogger
{
    public void Info(string message)
    {
        Log.Information(message);
    }

    public void Error(string message, Exception exception)
    {
        Log.Error(exception, message);
    }

    public void Warn(string message)
    {
        Log.Warning(message);
    }
}

