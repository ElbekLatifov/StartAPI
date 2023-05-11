namespace Api.Services;

public class Logger : ILogger
{
    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public void Log(string text, EnumL level = EnumL.Information)
    {
        File.AppendAllText($"LogHistoy{DateTime.Now:dd-MM-yyyy ss-mm}.txt", text + $" Date:{DateTime.Now:g}, {level}\n");
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        File.AppendAllText($"LogHistoy{DateTime.Now:dd-MM-yyyy ss-mm}.txt", logLevel + $" Date:{DateTime.Now:g}, {formatter(state, exception)}\n");
    }
}

public enum EnumL
{
    Information,
    Warning,
    Error
}