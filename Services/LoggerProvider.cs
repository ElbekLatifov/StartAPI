namespace Api.Services;

public class LoggerProvider : ILoggerProvider //obyekylar saqlanadi. Kerakli loggerlarni boshqaruvchi
{
    public ILogger CreateLogger(string categoryName)
    {
        return new Logger();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}